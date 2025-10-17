using System.Collections.Generic;
using UnityEngine;

public class ConveyorPath : MonoBehaviour
{
    [Tooltip("Ordered list of corners making up the conveyor loop (must be closed).")]
    public List<Transform> cornerPoints;

    private List<float> cumulativeLengths = new List<float>();
    private float totalLength;

    [Header("State")]
    public bool isActive = true;

    public GameObject leverOn;
    public GameObject leverOff;

    void Awake()
    {
        RecalculatePath();
    }

    public void RecalculatePath()
    {
        cumulativeLengths.Clear();
        totalLength = 0f;

        for (int i = 0; i < cornerPoints.Count; i++)
        {
            Vector3 a = cornerPoints[i].position;
            Vector3 b = cornerPoints[(i + 1) % cornerPoints.Count].position;

            totalLength += Vector3.Distance(a, b);
            cumulativeLengths.Add(totalLength);
        }
    }

    public Vector3 GetPosition(float distance)
    {
        if (cornerPoints.Count < 2) return Vector3.zero;

        distance %= totalLength;

        for (int i = 0; i < cornerPoints.Count; i++)
        {
            float prevLength = (i == 0) ? 0f : cumulativeLengths[i - 1];

            if (distance <= cumulativeLengths[i])
            {
                Vector3 a = cornerPoints[i].position;
                Vector3 b = cornerPoints[(i + 1) % cornerPoints.Count].position;

                float segLength = cumulativeLengths[i] - prevLength;
                float t = (distance - prevLength) / segLength;

                return Vector3.Lerp(a, b, t);
            }
        }

        return cornerPoints[0].position; // fallback
    }

    public float TotalLength => totalLength;

    // These methods can be called by a lever script
    public void TurnOn() => isActive = true;
    public void TurnOff() => isActive = false;
    public void Toggle() => isActive = !isActive;
}