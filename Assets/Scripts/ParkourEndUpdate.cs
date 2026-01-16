using UnityEngine;

public class ParkourEndUpdate : MonoBehaviour
{
    [SerializeField] private ParkourTask task;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            task.taskDone = true;
        }
    }

}
