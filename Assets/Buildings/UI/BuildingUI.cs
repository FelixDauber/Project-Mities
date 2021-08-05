using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [Header("BuildingInfo")]
    public Building targetBuilding;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text typeText;

    [Header("Production")]
    [SerializeField]
    Text recipeText;
    [SerializeField]
    Slider productionSlider;

    [Header("Removal")]
    [SerializeField]
    Button destroyButton;
    public UnityEngine.Events.UnityEvent<BuildingUI> OnRemoval;

    public void Setup(Building targetBuilding)
    {
        this.targetBuilding = targetBuilding;
        typeText.text = targetBuilding.type.name;
        nameText.text = targetBuilding.gameObject.name;
        targetBuilding.ProductionUpdate.AddListener(UpdateProduction);
        targetBuilding.NewProduction.AddListener(ChangeProduction);
        targetBuilding.WhenDestroyed.AddListener(Destroy);
        destroyButton.onClick.AddListener(delegate { Destroy(targetBuilding.gameObject); });
        ChangeProduction(targetBuilding.currentlyProducing);
    }

    void UpdateProduction(float production)
    {
        productionSlider.value = production / targetBuilding.currentlyProducing.effortRequired;
    }
    void ChangeProduction(Recipe newRecipe)
    {
        if (newRecipe != null)
            recipeText.text = "Producing: " + newRecipe.name;
        else
            recipeText.text = "Not Producing";
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        targetBuilding.ProductionUpdate.RemoveListener(UpdateProduction);
        targetBuilding.NewProduction.RemoveListener(ChangeProduction);
        targetBuilding.WhenDestroyed.RemoveListener(Destroy);
        OnRemoval.Invoke(this);
    }
}
