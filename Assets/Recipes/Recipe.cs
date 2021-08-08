using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Recipes/Recipe")]
public class Recipe : ScriptableObject
{
    public float effortRequired;
    public StructItem[] required;
    public StructItem produced;
}
