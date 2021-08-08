using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public IItem[] Items { get; }

    ///<summary> Maximum amount of items possible in the storage, Setting this to -1 makes the storage "infinite" </summary>
    public int MaxStorage { get; set; }

    ///<summary> Adds item to inventory, returns false if unsucessfull </summary>
    public bool AddItem(IItem item);

    ///<summary> Removes item from inventory, returns false if unsucessfull </summary>
    public bool RemoveItem(IItem item);

    ///<summary> Finds the specified item and returns it's values within the inventory, returns as amount 0 if not found </summary>
    public IItem FindItem(IItem item);

    ///<summary> Checks if the inventory contains the item and the amount specified </summary>
    public bool Contains(IItem item);

    public int GetRemainingSpace();

    public void Clear();
}
