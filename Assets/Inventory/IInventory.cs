using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public StructItem[] Items { get; }

    ///<summary> Adds item to inventory, returns false if unsucessfull </summary>
    public bool AddItem(StructItem item);

    ///<summary> Removes item from inventory, returns false if unsucessfull </summary>
    public bool RemoveItem(StructItem item);

    ///<summary> Finds the specified item and returns it's values within the inventory, returns as amount 0 if not found </summary>
    public StructItem FindItem(StructItem item);

    ///<summary> Checks if the inventory contains the item and the amount specified </summary>
    public bool Contains(StructItem item);
}
