using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditing : MonoBehaviour
{
    public Terrain terrain;
    public PlayerInteractor playerInteractor;
    public float raisePower = 0.001f;
    public int size = 5;
    public EditMode editMode;

    public enum EditMode
    {
        raise,
        lower,
        flatten,
        smoothen
    }

    void Start()
    {
        playerInteractor = FindObjectOfType<PlayerInteractor>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
            RaiseTerrain(playerInteractor.HitInfo.point);
    }

    void RaiseTerrain(Vector3 position)
    {
        //if (!IsWithinBounds(position)) return;

        position = GetTerrainCordinates(position);

        position.x -= size / 2;
        position.z -= size / 2;

        float[,] heightEdit = new float[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                heightEdit[x, z] = (raisePower * Time.deltaTime / terrain.terrainData.size.y) + (terrain.terrainData.GetHeight(Mathf.RoundToInt(position.x) + z, Mathf.RoundToInt(position.z) + x) / terrain.terrainData.size.y);
            }
        }

        //float[,] heightEdit = { { raisePower + (terrain.terrainData.GetHeight(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)) / terrain.terrainData.size.y) } };
        terrain.terrainData.SetHeights(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z), heightEdit);
    }

    Vector3 GetTerrainCordinates(Vector3 worldPosition)
    {
        Vector3 terrainPosition = terrain.GetPosition();
        Vector3 terrainCordinates = new Vector3(0,0,0);
        terrainCordinates.x = (worldPosition.x - terrainPosition.x) / terrain.terrainData.size.x;
        terrainCordinates.z = (worldPosition.z - terrainPosition.z) / terrain.terrainData.size.z;
        terrainCordinates *= terrain.terrainData.heightmapResolution;
        return terrainCordinates;
    }

    bool IsWithinBounds(Vector3 position)
    {
        if (position.x < 0 || position.z < 0) return false;

        if (position.x > terrain.terrainData.size.x) return false;
        if (position.z > terrain.terrainData.size.z) return false;

        return true;
    }
}
