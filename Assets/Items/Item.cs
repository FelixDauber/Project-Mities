using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData type;
    public int amount;
    public Item(Item item)
    {
        type = item.type;
        amount = item.amount;
    }
}
