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
    public float moveSpeed = 0.4f;
    public bool canPull = true;

    private bool playerInTrigger = false;
    private int currentDirection = 0; // +1 = A→B, -1 = B→A

    private void Update()
    {
        if (!canPull) return;
        if (playerInTrigger && Input.GetMouseButton(0) && Input.GetKey(KeyCode.S))
        {
            if (currentDirection == 1)
            {
                MoveTowards(pointB.position);
            }
            else if (currentDirection == -1)
            {
                MoveTowards(pointA.position);
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        box.position = Vector3.MoveTowards(box.position, target, moveSpeed * Time.deltaTime);
    }

    public void SetTriggerActive(bool active, int direction)
    {
        playerInTrigger = active;
        currentDirection = direction;
        if(playerInTrigger) TXT_input.SetText("[Mouse 1] Walk backwards to pull.");
        else if (!playerInTrigger) TXT_input.SetText("");
    }
}