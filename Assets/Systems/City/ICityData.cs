using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICityData
{
    public IResourceRequester ResourceRequester { get; }
    public IBuildingManager BuildingManager { get; }
    public ICitizenManager CitizenManager { get; }
}

public interface IResourceRequester
{
    public IResourceTicket CreateTradeTicket(IItem item, Transform requesterTransform, IInventory requesterInventory, bool buying);
}

public interface IResourceTicket
{
    public IItem Item { get; }

    public Transform FromTransform { get; }
    public IInventory FromInventory { get; }

    public Transform ToTransform { get; }
    public IInventory ToInventory { get; }

    public bool IsValid();
}

public interface IBuildingManager
{
    public List<IBuilding> Buildings { get; }

    public void AddBuilding(IBuilding building);
    public void RemoveBuilding(IBuilding building);

    public UnityEvent OnBuildingAdd { get; }
    public UnityEvent OnBuildingRemove { get; }
}

public interface IBuilding
{
    public UnityEvent OnCreated(IBuilding building);
    public UnityEvent OnDestroyed(IBuilding building);
}

public interface ICitizenManager
{
    public ICitizen[] Citizens { get; }

    public void AddCitizen(ICitizen citizen);
    public void RemoveCitizen(ICitizen citizen);
}

public interface ICitizen
{
    public UnityEvent OnCreated(ICitizen citizen);
    public UnityEvent OnDestroyed(ICitizen citizen);
}
