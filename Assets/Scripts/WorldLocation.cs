using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldLocation : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject prestory1;
    [SerializeField] private GameObject prestory2;

    private string currentArea;
    private IEnumerator coroutine;

    [SerializeField] private int storyStep = -1;
    private bool pregameScreen1 = true;
    private bool pregameScreen2 = true;

    private void Start()
    {
        prestory1.SetActive(true);
    }

    private void Update()
    {
        if(pregameScreen1 && Input.GetMouseButtonDown(0))
        {
            pregameScreen1 = false;
            prestory1.SetActive(false);
            prestory2.SetActive(true);
        }
        else if(pregameScreen2 && Input.GetMouseButtonDown(0))
        {
            pregameScreen2 = false;
            prestory2.SetActive(false);
        }
    }

    public void AreaMessage(string area, string text)
    {
        if (currentArea != area)
        {
            currentArea = area;
            TXT_notif.SetText(text);
            coroutine = ClearNotif(3f);
            StartCoroutine(coroutine);
        }
    }
    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
    private IEnumerator ClearStory(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_story.SetText("");
    }

    public void StepInStory(int step)
    {
        if (step > storyStep)
        {
            storyStep = step;
            SpeakStory();
        }
        else Debug.Log("Step " + step + " is behind the current step. The current step is " + storyStep + ".");
    }

    public void SpeakStory()
    {
        switch (storyStep)
        {
            case 1:
                TXT_story.SetText("Escape the garage.");
                break;
            case 2:
                TXT_story.SetText("Escape the garden.");
                break;
            case 3:
                TXT_story.SetText("Find someone for directions.");
                break;
            case 4:
                TXT_story.SetText("Find this 'berd' person.");
                break;
            case 5:
                TXT_story.SetText("Bring Berd's marbles to Alice.");
                break;
        }
    }
}
