using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Stag : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private int itemsNeeded = 15;
    [SerializeField] private GameObject blockade;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool satisfyTask = false;
    private bool playerInRange = false;
    private bool hasTalked = false; // first time talk prompt

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (hasTalked)
        {
            if (playerInRange && collectManager.collectedBones >= itemsNeeded)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    satisfyTask = true;
                    blockade.SetActive(false);
                    collectManager.Increase("bone", 1);
                    locationManager.StepInStory(4);
                    TXT_notif.SetText("Okay, impressive. I'll let you pass this time. Go find Berd, he'll help you.");
                    TXT_input.SetText("");
                    StartCoroutine(coroutine);
                    lightBeam.SetActive(false);
                }
            }
            else if (playerInRange && collectManager.collectedBones < itemsNeeded)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TXT_notif.SetText("I said you need " + itemsNeeded + " bones! Beat it!");
                    StartCoroutine(coroutine);
                }
            }
        }
        else if (!hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("The sanctuary is this way! But I won't let you pass until you have " + itemsNeeded + " bones.");
            hasTalked = true;
            StartCoroutine(coroutine);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            TXT_input.SetText("[E] Talk to " + npcInfo.charName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            TXT_input.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
