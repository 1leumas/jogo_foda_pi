using UnityEngine;

public class BuildingVisibility : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] roofs;

    public Terrain terrain;
    public Material darkMat;
    public LayerMask buildingFloorLayer;
    public Transform player;

    private bool isInsideBuilding = false;
    private Material originalMat;

    void Start() {
        originalMat = terrain.materialTemplate;
    }
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
                terrain.materialTemplate = originalMat;
            }
        }
        else
        {
            foreach (GameObject wall in walls)
            {
                wall.transform.position -= new Vector3(0, 5.5f, 0);
                terrain.materialTemplate = darkMat;
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
