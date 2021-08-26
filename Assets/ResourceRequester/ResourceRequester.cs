using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceRequester : MonoBehaviour, IResourceRequester
{
    public List<IInventory> inventories = new List<IInventory>();

    void Awake()
    {
        GetComponent<BuildingEventHandler>().SubscribeTo<BuildingEventHandler.EventBuildingConstructed>(AddInventory);
    }

    public void AddInventory(BuildingEventHandler.EventBuildingConstructed newBuilding)
    {
        inventories.Add(newBuilding.building.GetComponent<IInventory>());
    }

    public IResourceTicket CreateTradeTicket(IItem item, Transform requesterTransform, IInventory requesterInventory, bool buying = true)
    {
        if (buying)
        {
            IInventory bestSeller = GetHighestSeller(item, out IItem highestItem);
            if (bestSeller == null) return new TradeTicket(null, null, null, null);

            if (item.Amount > highestItem.Amount)
            {
                item.Amount = highestItem.Amount;
            }

            return new TradeTicket(requesterTransform, requesterInventory, bestSeller, item);
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    IInventory GetHighestSeller(IItem item, out IItem returnedItem)
    {
        IInventory bestInventory = null;
        returnedItem = null;
        int amountOfResources = 0;
        foreach (var inventory in inventories)
        {
            IItem foundItem = inventory.FindItem(item);
            if (foundItem != null && foundItem.Amount > amountOfResources)
            {
                bestInventory = inventory;
                amountOfResources = foundItem.Amount;
                returnedItem = foundItem;
            }
        }
        return bestInventory;
    }
}

public struct TradeTicket : IResourceTicket
{
    public Transform buyerTransform;
    public IInventory seller, buyer;

    //Item
    public IItem item; public IItem Item => throw new System.NotImplementedException();

    //From
    public Transform FromTransform => throw new System.NotImplementedException();
    public IInventory FromInventory => throw new System.NotImplementedException();

    //To
    public Transform ToTransform => buyerTransform;
    public IInventory ToInventory => throw new System.NotImplementedException();

    public TradeTicket(Transform buyerTransform, IInventory buyer, IInventory seller, IItem item)
    {
        this.buyerTransform = buyerTransform;
        this.buyer = buyer;
        this.seller = seller;
        this.item = item;
    }

    public bool IsValid()
    {
        if (buyerTransform == null || buyer == null || seller == null || item == null) return false;
        return true;
    }
}