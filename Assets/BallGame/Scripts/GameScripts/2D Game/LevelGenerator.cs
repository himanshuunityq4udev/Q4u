using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject prefabObject;

    private void Awake()
    {
        switch (Random.Range(0, 3))
        {
            case 0: PatternCircle(); break;
            case 1: PatternPyramid(); break;
            case 2: Pattern1(); break;
        }
    }

    public void Pattern1()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject spawnedObject = Instantiate(prefabObject, transform);
                spawnedObject.transform.position = transform.position + new Vector3((float)((size.x - 1) * .5f - i) * offset.x, j * offset.y, 0);
            }
        }
    }

    public void PatternPyramid()
    {
        for (int i = 0; i < size.y; i++) // size.y for pyramid height
        {
            int numInRow = size.x - i; // Decrease the number of objects per row as i increases
            float offsetX = (numInRow - 1) * 0.5f * offset.x; // Center the row horizontally

            for (int j = 0; j < numInRow; j++)
            {
                GameObject spawnedObject = Instantiate(prefabObject, transform);
                spawnedObject.transform.position = transform.position
                                                   + new Vector3(j * offset.x - offsetX, i * offset.y, 0);
            }
        }
    }

    public void PatternCircle()
    {
        int totalObjects = (int)size.x * size.y; // Total number of objects in the circle
        float radius = offset.x + 1; // Distance from the center

        for (int i = 0; i < totalObjects; i++)
        {
            // Calculate angle for each object
            float angle = i * Mathf.PI * 2 / totalObjects;

            // Determine x and y position based on angle
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Instantiate and position the object
            GameObject spawnedObject = Instantiate(prefabObject, transform);
            spawnedObject.transform.position = transform.position + new Vector3(x, y, 0);
        }
    }


}
