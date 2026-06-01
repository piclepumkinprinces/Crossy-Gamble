using UnityEngine;
using System.Collections.Generic; 

public class MapScript : MonoBehaviour
{
    int nextSpawnZ;
    public GameObject prototypeRows;
    public PlayerMovementSystem PlayerMovementSystem;

    Queue<GameObject> spawnedRows = new Queue<GameObject>();
    int maxRows = 20;

    void Start()
    {
        nextSpawnZ = 2;
    }

    void Update()
    {
        if (nextSpawnZ < 21)
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
        int rowChooser = Random.Range(0, 1);
        
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
        }
    }
}