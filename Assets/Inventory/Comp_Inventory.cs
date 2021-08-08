using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Inventory : MonoBehaviour, IInventory
{
    public IItem[] Items => items.ToArray();

    List<IItem> items = new List<IItem>();

    public int MaxStorage { get => maxStorage; set => maxStorage = value; }

    [SerializeField]
    int maxStorage = -1;

    public bool AddItem(IItem item)
    {
        IItem foundItem = FindItem(item);
        if (foundItem.Amount == 0)
        {
            items.Add(item);
        }
        return true;
    }

    public bool RemoveItem(IItem item)
    {
        IItem foundItem = FindItem(item);
        if (foundItem.Amount < item.Amount) return false;

        foreach (var examinedItem in items)
        {
            if (examinedItem.SameAs(item))
            {
                if (examinedItem.Amount < item.Amount) return false;

                examinedItem.Amount -= item.Amount;

                if(examinedItem.Amount == 0)
                    items.Remove(examinedItem);

                return true;
            }
        }
        return false;
    }

    public IItem FindItem(IItem item)
    {
        foreach (var examinedItem in items)
        {
            if (examinedItem.SameAs(item))
            {
                return examinedItem;
            }
        }
        item.Amount = 0;
        return item;
    }

    public bool Contains(IItem item)
    {
        IItem foundItem = FindItem(item);
        if (foundItem.Amount < item.Amount) return false;
        return true;
    }

    public int GetRemainingSpace()
    {
        int totalAmount = 0;
        foreach (var item in items)
        {
            totalAmount += item.Amount;
        }
        if (maxStorage == -1) return int.MaxValue - totalAmount;

        return maxStorage - totalAmount;
    }

    public void Clear()
    {
        items = new List<IItem>();
    }
}
