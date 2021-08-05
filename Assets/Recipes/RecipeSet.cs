using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Recipes/RecipeSet")]
public class RecipeSet : ScriptableObject
{
    [SerializeField]
    private List<Recipe> recipes;
}
