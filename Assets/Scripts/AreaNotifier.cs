using UnityEditor.Analytics;
using UnityEngine;

public class AreaNotifier : MonoBehaviour
{

    public WorldLocation manager;

    [Header("Task Settings")]
    [Tooltip("Is it an AreaBox")]
    public bool areaBox = false;
    [Tooltip("One Time message?")]
    public bool sendOnce;
    [Tooltip("Area Name")]
    public string areaName;
    [Tooltip("Area-Entry Message.")]
    public string entryMessage;

    [Header("Story Settings")]
    [Tooltip("Is it a StoryBox")]
    public bool storyBox = false;
    [Tooltip("What Story-part is it")]
    public int storyPart = 0;

    private bool sentStory = false;
    private bool sentArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!sentArea && areaBox)
            {
                if (sendOnce)
                {
                    sentArea = true;
                }
                manager.AreaMessage(areaName, entryMessage);
            }
            if (!sentStory && storyBox)
            {
                manager.StepInStory(storyPart);
                sentStory = true;
            }
        }
    }
}
