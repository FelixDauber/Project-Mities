using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventoryTracker : MonoBehaviour
{
    public List<IInventory> inventories = new List<IInventory>();
    IInventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Comp_Inventory>();
        if(inventory == null)
        {
            inventory = gameObject.AddComponent<Comp_Inventory>();
            inventory.MaxStorage = -1;
        }
    }

    [ContextMenu("Get Present Inventories")]
    public void GetPresentInventories()
    {
        inventories.Clear();
        foreach (var inventory in FindObjectsOfType<Comp_Inventory>())
        {
            inventories.Add(inventory);
        }
    }

    [ContextMenu("Calculate Inventory")]
    public void CalculateInventory()
    {
        inventory.Clear();
        foreach(IInventory inventory in inventories)
        {
            foreach (var item in inventory.Items)
            {
                this.inventory.AddItem(item);
            }
        }
    }
}
