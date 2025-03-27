using UnityEngine;

public class BuildingVisibility : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] roofs;

    public LayerMask buildingFloorLayer; // Layer do chão do prédio
    public Transform player; // Referência ao Player (arraste no Inspector)

    private bool isInsideBuilding = false;

    void Update()
    {
        CheckIfPlayerIsInside();
    }

    void CheckIfPlayerIsInside()
    {
        if (player == null) return; // Segurança para evitar erro

        RaycastHit hit;

        Vector3 rayOrigin = player.position + Vector3.up * 0.5f;

        // Dispara o Raycast para baixo a partir do Player
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 2f, buildingFloorLayer))
        {
            if (!isInsideBuilding)
            {
                SetBuildingVisibility(false);
                isInsideBuilding = true;
            }
        }
        else
        {
            if (isInsideBuilding)
            {
                SetBuildingVisibility(true);
                isInsideBuilding = false;
            }
        }
    }

    void SetBuildingVisibility(bool isVisible)
    {
        if (isVisible)
        {
            foreach (GameObject wall in walls)
            {
                wall.transform.position = new Vector3(wall.transform.position.x, 0, wall.transform.position.z);
            }
        }
        else
        {
            foreach (GameObject wall in walls)
            {
                wall.transform.position -= new Vector3(0, 5.5f, 0);
            }
        }

        foreach (GameObject roof in roofs)
        {
            roof.SetActive(isVisible);
        }
    }

    // Visualiza o Raycast na Scene para debug
    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(player.position, player.position + Vector3.down * 2f);
        }
    }
}
