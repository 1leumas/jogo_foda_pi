using Unity.Mathematics;
using UnityEngine;

public class BuildingVisibility : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] roofs;
    public GameObject floorsParent;

    public Terrain terrain;
    public LayerMask buildingFloorLayer;
    public Transform player;

    private bool isInsideBuilding = false;

    void Update()
    {
        CheckIfPlayerIsInside();
    }

    void CheckIfPlayerIsInside()
    {
        RaycastHit hit;
        Vector3 rayOrigin = player.position + Vector3.up * 0.5f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 5f, buildingFloorLayer) && IsHitAChildOfFloors(hit.collider.gameObject))
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

    bool IsHitAChildOfFloors(GameObject hitObject)
    {
        foreach (Transform child in floorsParent.transform)
        {
            if (hitObject == child.gameObject)
            {
                return true;
            }
        }
        return false;
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

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(player.position, player.position + Vector3.down * 2f);
        }
    }
}
