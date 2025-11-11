using UnityEngine;

public class RaceInteract : MonoBehaviour
{
    public bool isInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false;
        }
    }
}
