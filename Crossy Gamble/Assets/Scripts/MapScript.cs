using UnityEngine;
using System.Collections.Generic; 

public class MapScript : MonoBehaviour
{
    int nextSpawnZ;
    public GameObject prototypeRows;
    public PlayerMovementSystem PlayerMovementSystem;

    Queue<GameObject> spawnedRows = new Queue<GameObject>();
    int maxRows;

    void Start()
    {
        nextSpawnZ = 2;
        maxRows = 32;
    }

    void Update()
    {
        if (spawnedRows.Count > maxRows)
        {
            SpawnRow();
        }

        if (PlayerMovementSystem.isMovingForward == 1)
        {
            SpawnRow();
            DeleteOldestRow();  
        }
    }

    void SpawnRow()
    {
        int rowChooser = Random.Range(0, 2);
        
        GameObject newRow = Instantiate(prototypeRows.transform.GetChild(rowChooser).gameObject, new Vector3(0, 0, nextSpawnZ), Quaternion.Euler(0, 90, 0));

        spawnedRows.Enqueue(newRow);
        nextSpawnZ += 2;
    }

    void DeleteOldestRow()
    {
        if (spawnedRows.Count > maxRows)
        {
            GameObject oldRow = spawnedRows.Dequeue();
            Destroy(oldRow);
            PlayerMovementSystem.isMovingForward = 0;
        }
    }
}