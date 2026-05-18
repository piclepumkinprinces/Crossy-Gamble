using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float stepSize;
    public bool isMoving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stepSize = 2;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.x = stepSize;
            isMoving = true;
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.z = stepSize;   
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.z = -stepSize;
        }

        transform.position += movement;
        if (movement == Vector3.zero)
        {
            isMoving = false;
        }
    }
}
