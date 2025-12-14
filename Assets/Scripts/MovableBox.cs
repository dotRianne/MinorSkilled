using TMPro;
using UnityEngine;

public class MovableBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform box;        // The actual box mesh
    [SerializeField] private Transform pointA;     // Start position
    [SerializeField] private Transform pointB;     // End position
    public TMP_Text TXT_input;

    [Header("Settings")]
    public float walkSpeed = 2f;
    public float sprintSpeed = 6.6f;
    public bool canPull = true;

    private float currentSpeed = 0f;
    private bool playerInTrigger = false;
    private int currentDirection = 0; // +1 = A→B, -1 = B→A
    private float percentAlong = 0f;
    private float distABx = 0f;
    private float distABy = 0f;
    private float distABz = 0f;

    private void Start()
    {
        if (pointA.position.x >= pointB.position.x + 0.1f || pointA.position.x <= pointB.position.x - 0.1f)
        {
            if (pointA.position.x > pointB.position.x) distABx = pointA.position.x - pointB.position.x;
            else distABx = pointB.position.x - pointA.position.x;
        }
        else if (pointA.position.y >= pointB.position.y + 0.1f || pointA.position.y <= pointB.position.y - 0.1f)
        {
            if (pointA.position.y > pointB.position.y) distABy = pointA.position.y - pointB.position.y;
            else distABy = pointB.position.y - pointA.position.y;
        }
        else if (pointA.position.z >= pointB.position.z + 0.1f || pointA.position.z <= pointB.position.z - 0.1f)
        {
            if (pointA.position.z > pointB.position.z) distABz = pointA.position.z - pointB.position.z;
            else distABz = pointB.position.z - pointA.position.z;
        }
    }


    private void Update()
    {
        if (!canPull) return;

        if (currentDirection == -1 && percentAlong <= 0.1) TXT_input.SetText("");
        else if (currentDirection == 1 && percentAlong >= 0.9) TXT_input.SetText("");
        else if (playerInTrigger) TXT_input.SetText("[Mouse 1] Walk backwards to pull.");

        if (playerInTrigger && Input.GetMouseButton(0) && Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = sprintSpeed;
            else currentSpeed = walkSpeed;

            if (currentDirection == 1)
            {
                MoveTowards(pointB.position);
                if (distABx > 0 && percentAlong <= 1) percentAlong += (currentSpeed * Time.deltaTime) / distABx;
                else if (distABy > 0 && percentAlong <= 1) percentAlong += (currentSpeed * Time.deltaTime) / distABy;
                else if (distABz > 0 && percentAlong <= 1) percentAlong += (currentSpeed * Time.deltaTime) / distABz;
            }
            else if (currentDirection == -1)
            {
                MoveTowards(pointA.position);
                if (distABx > 0 && percentAlong >= 0) percentAlong -= (currentSpeed * Time.deltaTime) / distABx;
                else if (distABy > 0 && percentAlong >= 0) percentAlong -= (currentSpeed * Time.deltaTime) / distABy;
                else if (distABz > 0 && percentAlong >= 0) percentAlong -= (currentSpeed * Time.deltaTime) / distABz;
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        box.position = Vector3.MoveTowards(box.position, target, currentSpeed * Time.deltaTime);
    }

    public void SetTriggerActive(bool active, int direction)
    {
        playerInTrigger = active;
        currentDirection = direction;
        if(playerInTrigger) TXT_input.SetText("[Mouse 1] Walk backwards to pull.");
        else if (!playerInTrigger) TXT_input.SetText("");
    }
}