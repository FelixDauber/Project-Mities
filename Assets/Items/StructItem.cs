using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructItem : IItem
{
    [SerializeField]
    ItemData itemData;
    public ItemData ItemData => itemData;

    [SerializeField]
    int amount;
    public int Amount { get => amount; set => amount = value; }

    public StructItem(ItemData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }

    public bool SameAs(IItem other)
    {
        if ((other.GetType() != GetType()) || other.ItemData != ItemData)
        {
            return false;
        }
        return true;
    }
}
