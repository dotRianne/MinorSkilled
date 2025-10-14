using UnityEngine;

public class ConveyorBox : MonoBehaviour
{
    public ConveyorPath path;
    public float speed = 2f;
    public float distanceOffset = 0f;

    private float distanceTravelled;

    void Start()
    {
        if (path == null)
        {
            Debug.LogError($"{name} has no ConveyorPath assigned!");
            enabled = false;
        }

        distanceTravelled = distanceOffset;
    }

    void Update()
    {
        if (path.isActive)
        {
            distanceTravelled += speed * Time.deltaTime;
        }

        transform.position = path.GetPosition(distanceTravelled);
    }
}