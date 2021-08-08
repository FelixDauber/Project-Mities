using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StructItem
{
    public ItemData itemData;
    public override bool Equals(object obj)
    {
        if (!(obj is StructItem) || ((StructItem)obj).itemData != itemData)
            return false;

        return Equals((StructItem)obj);
    }
}
