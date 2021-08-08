using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGetter : MonoBehaviour
{
    [SerializeField]
    int TMPAmountOfBuildings;
    [SerializeField]
    bool debugInhabit = false;

    public GameObject UIPrefab;
    private List<GameObject> currentlyActive = new List<GameObject>();
    private BuildingPlacing buildingPlacing;

    private void Awake()
    {
        buildingPlacing = FindObjectOfType<BuildingPlacing>();
    }

    private void OnEnable()
    {
        SpawnUIElements();
    }

    private void OnDisable()
    {
        DestroyUIElements();
    }

    void SpawnUIElements()
    {
        foreach(GameObject building in GetBuildings())
        {
            GameObject newButton = Instantiate(UIPrefab, transform);
            currentlyActive.Add(newButton);
            newButton.GetComponentInChildren<UnityEngine.UI.Text>().text = building.name;
            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SpawnBuilding(building); });
        }
        /*
        for (int i = 0; i < TMPAmountOfBuildings; i++)
        {
            currentlyActive.Add(Instantiate(UIPrefab, transform));
        }
        */
        if(debugInhabit)
            SpawnDebugInstances();
    }

    void SpawnDebugInstances()
    {
        for (int i = 0; i < TMPAmountOfBuildings; i++)
        {
            GameObject newButton = Instantiate(UIPrefab, transform);
            currentlyActive.Add(newButton);
        }
    }

    void SpawnBuilding(GameObject building)
    {
        buildingPlacing.SpawnBuilding(building);
    }

    void DestroyUIElements()
    {
        foreach (GameObject uIElement in currentlyActive)
        {
            Destroy(uIElement);
        }

    }

    GameObject[] GetBuildings()
    {
        Object[] objects = Resources.LoadAll("Buildings");

        List<GameObject> gameObjects = new List<GameObject>();
        foreach(Object @object in objects)
        {
            if(@object.GetType() == typeof(GameObject))
            {
                gameObjects.Add((GameObject)@object);
            }
        }
        return gameObjects.ToArray();// (GameObject[])objects;
    }
}
