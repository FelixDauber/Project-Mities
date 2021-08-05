using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    public List<Item> items = new List<Item>();
    public int maxSize = 100;

    public bool AddItem(Item item)
    {
        Item foundItem = FindItem(item);
        if(foundItem == null)
        {
            items.Add(new Item(item));
        }
        else
        {
            foundItem.amount += item.amount;
        }
        return true;
    }

    public bool RemoveItem(Item item)
    {
        Item foundItem = FindItem(item);
        if(foundItem == null || foundItem.amount < item.amount)
        {
            return false;
        }
        else
            foundItem.amount -= item.amount;
        return true;
    }

    public Item FindItem(Item item)
    {
        foreach (var inventoryItem in items)
        {
            if (inventoryItem.type == item.type)
            {
                return inventoryItem;
            }
        }
        return null;
    }

    public bool ContainsItem(Item item)
    {
        foreach (var inventoryItem in items)
        {
            if (inventoryItem.type == item.type && inventoryItem.amount >= item.amount)
            {
                return true;
            }
        }
        return false;
    }

    public int GetTotal()
    {
        int total = 0;
        foreach (var inventoryItem in items)
        {
            total += inventoryItem.amount;
        }
        return total;
    }

    public int GetRemainingSpace()
    {
        return maxSize - GetTotal();
    }
}
