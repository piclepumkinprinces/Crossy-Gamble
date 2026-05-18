using UnityEngine;
using System.Collections.Generic; // Required for Lists or Queues

public class MapScript : MonoBehaviour
{
    int nextSpawnX;
    public GameObject prototypeRows;
    public PlayerScript playerScript;

    Queue<GameObject> spawnedRows = new Queue<GameObject>();
    int maxRows = 20;

    void Start()
    {
        nextSpawnX = 2;
    }

    void Update()
    {
        if (nextSpawnX < 21)
        {
            SpawnRow();
        }

        if (playerScript.isMoving)
        {
            SpawnRow();
            DeleteOldestRow();
        }
    }

    void SpawnRow()
    {
        int rowChooser = Random.Range(0, 11);
        
        GameObject newRow = Instantiate(prototypeRows.transform.GetChild(rowChooser).gameObject, new Vector3(nextSpawnX, 0, 0), Quaternion.identity);

        spawnedRows.Enqueue(newRow);
        nextSpawnX += 2;
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