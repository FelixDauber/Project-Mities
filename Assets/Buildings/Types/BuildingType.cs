using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building/BuildingType")]
public class BuildingType : ScriptableObject
{
    public bool IsProducer => recipes.Length > 0;
    public float RecipeEfficency
    {
        get
        {
            float efficiency = 1;
            for (int i = 0; i < recipes.Length; i++)
            {
                efficiency *= (1 - inefficiencyPerRecipe);
            }
            return Mathf.Pow(1 - inefficiencyPerRecipe, recipes.Length);
        }
    }
    private const float inefficiencyPerRecipe = 0.1f;
    public Recipe[] recipes;
}
