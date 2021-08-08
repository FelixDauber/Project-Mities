using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public ItemData ItemData { get; }
    public int Amount { get; set; }
    public bool SameAs(IItem other);
}
