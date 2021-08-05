using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUISpawner : MonoBehaviour
{
    public BuildingUI buildingUIPrefab;
    public Transform canvas;
    public List<BuildingUI> activeUIs;

    private void Awake()
    {
        FindObjectOfType<PlayerInteractor>().ClickedObject.AddListener(CreateUI);
    }

    private void CreateUI(RaycastHit hit)
    {
        Building building = hit.collider.gameObject.GetComponentInParent<Building>();

        if (building == null || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() || ActiveUisContainsBuilding(building) || !(PlayerVariables.current.playerState == Playerstate.none)) return;

        BuildingUI newUI = Instantiate(buildingUIPrefab, canvas);
        newUI.Setup(building);
        activeUIs.Add(newUI);
        newUI.OnRemoval.AddListener(RemoveUI);
    }

    bool ActiveUisContainsBuilding(Building building)
    {
        foreach(BuildingUI buildingUI in activeUIs)
        {
            if (buildingUI.targetBuilding == building) return true;
        }
        return false;
    }

    void RemoveUI(BuildingUI targetUI)
    {
        activeUIs.Remove(targetUI);
    }
}
