using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    [Tooltip("Set +1 to move from A to B, -1 to move from B to A")]
    public int direction = 1;
    public bool canPull = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canPull) return;
        if (other.CompareTag("Player"))
        {
            MovableBox box = GetComponentInParent<MovableBox>();
            if (box != null)
            {
                box.SetTriggerActive(true, direction);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!canPull) return;
        if (other.CompareTag("Player"))
        {
            MovableBox box = GetComponentInParent<MovableBox>();
            if (box != null)
            {
                box.SetTriggerActive(false, 0);
            }
        }
    }
}