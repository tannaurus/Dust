using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{

    public int worldSize = 100;
    public int buildingSize = 5;
    public int pathSize = 15;
    public GameObject building;
    public GameObject floor;

    private Vector2 grid;
    private GameObject[][] objectsGrid;

    // Start is called before the first frame update
    void Start()
    {
        Make();
    }

    void Make()
    {
        MakeFloor();
        int gridSize = worldSize;
        objectsGrid = new GameObject[worldSize][];
        for (int x = 0; x < gridSize; x++)
        {
            objectsGrid[x] = new GameObject[worldSize];
            if (x % 2 != 0) continue;
            for (int z = 0; z < gridSize; z++)
            {
                if (z % 2 != 0) continue;
                Vector3 pos = new Vector3((x * buildingSize) + pathSize, -5, (z * buildingSize) + pathSize);
                Quaternion rot = new Quaternion(0, 0, 0, 0);
                GameObject obj = Instantiate(building, pos, rot);
                objectsGrid[x][z] = obj;
            }
        }
    }

    void MakeFloor()
    {
        Vector3 pos = new Vector3(0, -10, 0);
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Transform floorTransform = floor.transform;
        floorTransform.localScale = new Vector3(worldSize / 2, 1, worldSize / 2);
        GameObject obj = Instantiate(floor, pos, rot, floorTransform);
    }
}
