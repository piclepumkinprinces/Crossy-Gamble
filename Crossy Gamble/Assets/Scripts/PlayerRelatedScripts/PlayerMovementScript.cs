using UnityEngine;
using System.Collections;

public class PlayerMovementSystem : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float DiceSize = 2;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float rollAngle = 90;
    [SerializeField] private float rollDuration = 0.3f;
    [SerializeField] public int isMovingForward;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask obstacleLayer; 

    private Vector3? queuedDirection = null;
    private bool isMoving = false;

    void Start()
    {
        isMovingForward = 0;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3? inputDir = null;

        if (Input.GetKeyDown(KeyCode.W)) inputDir = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.A)) inputDir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D)) inputDir = Vector3.right;

        if (!inputDir.HasValue) return;

        if (isMoving)
        {
            queuedDirection = inputDir;
            return;
        }

        TryMove(inputDir.Value);
    }

    void TryMove(Vector3 direction)
    {
        if (!CanMoveThere(direction)) 
        {
            queuedDirection = null; 
            return;
        }
        StartCoroutine(Roll(direction));
    }

    bool CanMoveThere(Vector3 dir)
    {
        Vector3 targetPosition = transform.position + (dir * DiceSize);
        Vector3 halfExtents = new Vector3(DiceSize, DiceSize, DiceSize) * 0.45f;
        bool isObstacleInWay = Physics.CheckBox(targetPosition, halfExtents, Quaternion.identity, obstacleLayer);
        return !isObstacleInWay;
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;


        if (direction == Vector3.forward)
        {
            isMovingForward = 1;
        }
        else
        {
            isMovingForward = 0;
        }

        float rotated = 0f;
        Vector3 pivot = transform.position + (Vector3.down + direction) * (DiceSize / 2f);
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        while (rotated < rollAngle)
        {
            float step = (rollAngle / rollDuration) * Time.deltaTime;

            if (rotated + step > rollAngle)
                step = rollAngle - rotated;

            transform.RotateAround(pivot, axis, step);
            rotated += step;

            yield return null;
        }

        SnapToGrid();

        isMoving = false;

        if (queuedDirection.HasValue)
        {
            Vector3 next = queuedDirection.Value;
            queuedDirection = null;
            TryMove(next);
        }
    }

    void SnapToGrid()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );

        transform.rotation = Quaternion.identity;
    }
}