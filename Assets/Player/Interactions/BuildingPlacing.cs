using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacing : MonoBehaviour
{
    private PlayerInteractor playerInteractor;
    private RaycastHit HitInfo => playerInteractor.HitInfo;

    public Building currentBuilding;

    //Testing
    public GameObject TMPBuildingPrefab;

    private void Awake()
    {
        playerInteractor = GetComponent<PlayerInteractor>();
    }

    void Update()
    {
        UpdatePosition();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlaceBuilding();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlacement();
        }
    }

    public void SpawnBuilding(GameObject newBuilding)
    {
        if (currentBuilding != null) 
        {
            Debug.Log(currentBuilding.gameObject);
            Destroy(currentBuilding.gameObject);
        }
        Building instantiatedBuilding = Instantiate(newBuilding).GetComponent<Building>();
        instantiatedBuilding.transform.position = Vector3.down * 50000;
        instantiatedBuilding.gameObject.name = instantiatedBuilding.gameObject.name.Replace("(Clone)", "");
        GrabBuilding(instantiatedBuilding, true);
    }

    public void GrabBuilding(Building building, bool forced = false)
    {
        if (!forced && currentBuilding != null) return;
        currentBuilding = building;
        currentBuilding.SetPhysical(false);
        PlayerVariables.current.playerState = Playerstate.placingBuilding;
        enabled = true;
    }

    void UpdatePosition()
    {
        if (HitInfo.collider == null) return;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentBuilding.transform.LookAt(new Vector3(HitInfo.point.x, currentBuilding.transform.position.y, HitInfo.point.z));
        }
        else
        {
            currentBuilding.transform.position = HitInfo.point;
        }
    }

    void CancelPlacement()
    {
        if(currentBuilding != null)
        {
            Destroy(currentBuilding.gameObject);
            enabled = false;
        }
    }

    void PlaceBuilding()
    {
        if (HitInfo.collider == null || EventSystem.current.IsPointerOverGameObject()) return;
        currentBuilding.SetPhysical(true);
        currentBuilding = null;
        PlayerVariables.current.playerState = Playerstate.none;
        enabled = false;
    }
}
