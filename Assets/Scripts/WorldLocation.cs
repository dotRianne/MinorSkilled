using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class WorldLocation : MonoBehaviour
{
    [SerializeField] private GameObject input_bg;
    [SerializeField] private GameObject notif_bg;
    [SerializeField] private GameObject extra_bg;

    [SerializeField] private AudioSource musicSource;
    //[SerializeField] private AudioClip musicClip;

    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private TMP_Text TXT_extra;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private GameObject prestory1;
    [SerializeField] private GameObject prestory2;
    [SerializeField] private CollectiblesManager collectManager;

    private string currentArea;
    private IEnumerator coroutine;

    [Header("Story")]
    private bool pregameScreen1 = true;
    private bool pregameScreen2 = true;

    public bool atParty = false;
    private bool setupParty = false;
    [SerializeField] private GameObject worldAnimals;
    [SerializeField] private GameObject partyAnimals;

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

    private void Start()
    {
        prestory1.SetActive(true);
        musicSource.loop = true;
        musicSource.Play();
    }

    private void Update()
    {
        if(atParty && !setupParty)
        {
            setupParty = true;
            worldAnimals.SetActive(false);
            partyAnimals.SetActive(true);
        }


        if (TXT_extra.text == "" && extra_bg.activeSelf == true) extra_bg.SetActive(false);
        else if (TXT_extra.text != "" && extra_bg.activeSelf == false) extra_bg.SetActive(true);

        if (TXT_input.text == "" && input_bg.activeSelf == true) input_bg.SetActive(false);
        else if (TXT_input.text != "" && input_bg.activeSelf == false) input_bg.SetActive(true);

        if (TXT_notif.text == "" && notif_bg.activeSelf == true) notif_bg.SetActive(false);
        else if (TXT_notif.text != "" && notif_bg.activeSelf == false) notif_bg.SetActive(true);


        if (pregameScreen1 && Input.GetMouseButtonDown(0))
        {
            pregameScreen1 = false;
            pregameScreen1 = true;
            prestory1.SetActive(false);
        }
        if(helpedDog && helpedAlpaca && helpedPenguins && helpedWolf && !helpedAllMarket)
        {
            helpedAllMarket = true;
            marketWall.SetActive(false);
        }
        if (helpedDeer && helpedDuck && helpedFox && !helpedAllStreets)
        {
            helpedAllStreets = true;
            streetsWall.SetActive(false);
        }
        if (helpedBird && helpedChicken && helpedFish && helpedSquirrel && !helpedAllConstruction)
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
}
