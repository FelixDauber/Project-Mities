using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Inventory : MonoBehaviour, IInventory
{
    public StructItem[] Items => items.ToArray();

    List<StructItem> items;

    public bool AddItem(StructItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool RemoveItem(StructItem item)
    {
        throw new System.NotImplementedException();
    }

    public StructItem FindItem(StructItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(StructItem item)
    {
        throw new System.NotImplementedException();
    }
}
