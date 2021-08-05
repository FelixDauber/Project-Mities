using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Itemset")]
public class ItemSet : ScriptableObject
{
    [SerializeField]
    private List<ItemData> items;
}
