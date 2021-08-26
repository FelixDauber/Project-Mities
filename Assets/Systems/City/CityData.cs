using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityData : MonoBehaviour, ICityData
{
    public static CityData main;
    public IResourceRequester ResourceRequester => resourceRequester;
    IResourceRequester resourceRequester;

    public IBuildingManager BuildingManager => buildingManager;
    IBuildingManager buildingManager;

    public ICitizenManager CitizenManager => citizenManager;
    ICitizenManager citizenManager;

    private void Awake()
    {
        if (main == null) main = this;
        else Destroy(this);

        if (ResourceRequester == null)
        {
            resourceRequester = GetComponent<ResourceRequester>();
        }
    }
}
