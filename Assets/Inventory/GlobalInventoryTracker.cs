using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventoryTracker : MonoBehaviour
{
    public List<Inventory> inventories = new List<Inventory>();
    public Inventory inventory = new Inventory(-1);

    [ContextMenu("Get Present Inventories")]
    public void GetPresentInventories()
    {
        inventories.Clear();
        foreach (var building in FindObjectsOfType<Building>())
        {
            inventories.Add(building.inventory);
        }
    }

    [ContextMenu("Calculate Inventory")]
    public void CalculateInventory()
    {
        inventory = new Inventory(-1);
        foreach(Inventory inventory in inventories)
        {
            foreach (var item in inventory.items)
            {
                this.inventory.AddItem(new Item(item));
            }
        }
    }
}
