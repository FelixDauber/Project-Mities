using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ResourceRequester : MonoBehaviour
{
    public List<Inventory> inventories;

    private void Awake()
    {
        foreach (var building in FindObjectsOfType<Building>())
        {
            inventories.Add(building.inventory);
        }
    }

    public TradeTicket RequestTradeTicket(Inventory buyer, Item item, bool selling)
    {
        if (!selling)
        {
            Inventory bestSeller = GetHighestSeller(item, out Item highestItem);
            if (bestSeller == null) return null;

            Item newItem = new Item(item);
            if (item.amount > highestItem.amount)
            {
                newItem.amount = item.amount;
            }
            else
            {
                newItem.amount = highestItem.amount;
            }

            return new TradeTicket(bestSeller, buyer, newItem);
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    Inventory GetHighestSeller(Item item, out Item returnedItem)
    {
        Inventory bestInventory = null;
        returnedItem = null;
        int amountOfResources = 0;
        foreach (var inventory in inventories)
        {
            Item foundItem = inventory.FindItem(item);
            if (foundItem != null && foundItem.amount > amountOfResources)
            {
                bestInventory = inventory;
                amountOfResources = foundItem.amount;
                returnedItem = foundItem;
            }
        }
        return bestInventory;
    }
}

public class TradeTicket
{
    public TradeTicket(Inventory giver, Inventory receiver, Item item)
    {
        this.giver = giver;
        this.receiver = receiver;
        this.item = item;
    }
    Inventory giver, receiver;
    Item item;
}
*/