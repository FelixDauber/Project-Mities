using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [Header("Structural")]
    public List<GameObject> structuralParts;
    public List<GameObject> decorativeParts;

    [Header("Size")]
    public float size;
    public int[] families;
    public int[] workers;

    [Header("Production")]
    public BuildingType type; //Determines recipes of the building
    [SerializeField]
    public Inventory inventory;
    public Recipe currentlyProducing;
    public float currentProduction;
    public float productionEfficiency;
    public UnityEvent<float> ProductionUpdate;
    public UnityEvent<Recipe> NewProduction;

    public UnityEvent WhenDestroyed;

    private void Awake()
    {
        productionEfficiency = type.RecipeEfficency;
    }

    private void Update()
    {
        Produce(Time.deltaTime);
    }

    void Produce(float amount)
    {
        if (currentlyProducing == null)
        {
            SetNextProduction();
            NewProduction.Invoke(currentlyProducing);
        }
        else if (currentProduction >= currentlyProducing.effortRequired)
        {
            inventory.AddItem(currentlyProducing.produced);
            ProductionUpdate.Invoke(0);
            currentProduction = 0;
            currentlyProducing = null;
        }
        else
        {
            currentProduction += amount * productionEfficiency;
            ProductionUpdate.Invoke(currentProduction);
        }
    }

    void SetNextProduction()
    {
        if (!type.IsProducer) return;
        Recipe bestRecipe = null;
        int value = 0;
        foreach (Recipe recipe in type.recipes)
        {
            int recipeValue = GetRecipeValue(recipe);
            if (recipeValue > value)
            {
                bestRecipe = recipe;
                value = recipeValue;
            }
        }

        if (bestRecipe != null)
        {
            currentlyProducing = bestRecipe;

            foreach (Item item in bestRecipe.required)
                inventory.RemoveItem(item);
        }
    }

    int GetRecipeValue(Recipe recipe)
    {
        int value = inventory.maxSize / type.recipes.Length;

        if(recipe.produced.amount > inventory.GetRemainingSpace())
        {
            return 0;
        }

        Item inventoryItem = inventory.FindItem(recipe.produced);

        if (inventoryItem != null)
        {
            value -= inventoryItem.amount;
        }

        if (value == 0) return 0;

        foreach(Item item in recipe.required)
        {
            if (!inventory.ContainsItem(item))
            {
                return 0;
            }
        }
        return value;
    }

    bool CheckIfRecipeViable(Recipe recipe)
    {
        if (!inventory.ContainsItem(recipe.produced))
        {
            foreach (Item item in recipe.required)
            {
                if (!inventory.ContainsItem(item))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void SetPhysical(bool state)
    {
        if (state)
        {
            enabled = true;
            foreach (GameObject part in structuralParts)
            {
                part.GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            enabled = false;
            foreach (GameObject part in structuralParts)
            {
                part.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        WhenDestroyed.Invoke();
    }
}
