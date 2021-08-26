using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingManager : IBuildingManager
{
    public List<IBuilding> Buildings => buildings;
    List<IBuilding> buildings;

    public UnityEvent OnBuildingAdd => onBuildingAdd;
    UnityEvent onBuildingAdd;
    public UnityEvent OnBuildingRemove => onBuildingRemove;
    UnityEvent onBuildingRemove;

    public void AddBuilding(IBuilding building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
        }
    }
    public void RemoveBuilding(IBuilding building)
    {

    }
}
