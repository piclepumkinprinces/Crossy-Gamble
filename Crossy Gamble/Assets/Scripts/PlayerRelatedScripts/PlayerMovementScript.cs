using UnityEngine;
using System.Collections;

public class PlayerMovementSystem : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float DiceSize = 1f;
    [SerializeField] private int maxSize = 10;
    [SerializeField] private float rollAngle = 90f;
    [SerializeField] private float rollDuration = 0.3f;
    [SerializeField] public bool isMovingForward;

    private Vector3? queuedDirection = null;
    private bool isMoving = false

    void Start()
    {
        isMovingForward = false;
    }
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3? inputDir = null;

        if (Input.GetKeyDown(KeyCode.W))
        {
            inputDir = Vector3.forward;
            isMovingForward = true;
        }
        else if (Input.GetKeyDown(KeyCode.A)) inputDir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D)) inputDir = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.S)) inputDir = Vector3.back;

        isMovingForward = false;

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
        if (!CanMoveThere(direction)) return;
        StartCoroutine(Roll(direction));
    }

    bool CanMoveThere(Vector3 dir)
    {
        Vector3 newPos = transform.position + dir * DiceSize;

        return Mathf.Abs(Mathf.RoundToInt(newPos.x)) < maxSize &&
               Mathf.Abs(Mathf.RoundToInt(newPos.z)) < maxSize;
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

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