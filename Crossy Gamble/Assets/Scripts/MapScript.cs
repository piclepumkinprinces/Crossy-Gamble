using UnityEngine;
using System.Collections.Generic; 
using TMPro;

public class MapScript : MonoBehaviour
{
    int nextSpawnZ;
    public GameObject prototypeRows;
    public PlayerMovementSystem PlayerMovementSystem;
    public TextMeshProUGUI scoreText;
    int score;

    Queue<GameObject> spawnedRows = new Queue<GameObject>();
    int maxRows;

    void Start()
    {
        nextSpawnZ = 2;
        maxRows = 33;
    }

    void Update()
    {
        score = PlayerMovementSystem.score;
        scoreText.text = "Score: " + score;

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
        int rowChooser = Random.Range(0, 3);
        
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