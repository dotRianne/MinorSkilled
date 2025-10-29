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
    [SerializeField] private CollectiblesManager collectManager;

    private string currentArea;
    private IEnumerator coroutine;

    [Header("Story")]
    [SerializeField] private int storyStep = -1;
    private bool pregameScreen1 = true;
    private bool pregameScreen2 = true;

    [HideInInspector] public bool helpedFox;
    [HideInInspector] public bool helpedDuck;
    [HideInInspector] public bool helpedDeer;
    [Header("Streets")]
    [SerializeField] private bool helpedAllStreets = false;
    [SerializeField] private GameObject streetsWall;
    public int streetsMinimumBone;

    [HideInInspector] public bool helpedPenguins;
    [HideInInspector] public bool helpedAlpaca;
    [HideInInspector] public bool helpedDog;
    [HideInInspector] public bool helpedWolf;
    [Header("Market")]
    [SerializeField] private bool helpedAllMarket = false;
    [SerializeField] private GameObject marketWall;
    public int marketMinimumBones;

    [HideInInspector] public bool helpedBird;
    [HideInInspector] public bool helpedSquirrel;
    [HideInInspector] public bool helpedChicken;
    [HideInInspector] public bool helpedFish;
    [Header("Construction")]
    [SerializeField] private bool helpedAllConstruction = false;
    [SerializeField] private GameObject constructionWall;
    public int constructionMinimumBones;

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
        if(helpedDog && helpedAlpaca && helpedPenguins && helpedWolf && !helpedAllMarket && collectManager.collectedBones >= marketMinimumBones)
        {
            helpedAllMarket = true;
            marketWall.SetActive(false);
        }
        if (helpedDeer && helpedDuck && helpedFox && !helpedAllStreets && collectManager.collectedBones >= streetsMinimumBone)
        {
            helpedAllStreets = true;
            streetsWall.SetActive(false);
        }
        if (helpedBird && helpedChicken && helpedFish && helpedSquirrel && !helpedAllConstruction && collectManager.collectedBones >= constructionMinimumBones)
        {
            helpedAllConstruction = true;
            constructionWall.SetActive(false);
        }
    }

    public void AreaMessage(string area, string text)
    {
        if (currentArea != area)
        {
            currentArea = area;
            TXT_notif.SetText(text);
            coroutine = ClearNotif(3f);
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }
    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
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
                TXT_story.SetText("Head to the market.");
                break;
            case 4:
                TXT_story.SetText("Head to the construction site.");
                break;
            case 5:
                TXT_story.SetText("Head to the Animal Sanctuary.");
                break;
            case 6:
                TXT_story.SetText("Head up to the bird.");
                break;
        }
    }
}
