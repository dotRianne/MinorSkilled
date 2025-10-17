using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Stag : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private int itemsNeeded = 5;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool satisfyTask = false;
    private bool hasRewarded = false;
    private bool playerInRange = false;
    private bool hasTalked = false; // first time talk prompt

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (hasTalked && playerInRange && !hasRewarded)
        {
            if (collectManager.collectedGems >= itemsNeeded)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    satisfyTask = true;
                    hasRewarded = true;
                    collectManager.Decrease("gem", itemsNeeded);
                    TXT_notif.SetText("Yippee! I love my gems!");
                    TXT_input.SetText("");
                    StartCoroutine(coroutine);
                    lightBeam.SetActive(false);
                    reward.SetActive(true);
                    locationManager.helpedDeer = true;
                }
            }
            else if (collectManager.collectedGems < itemsNeeded)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TXT_notif.SetText("Did you get the " + itemsNeeded + " gems yet?");
                    StartCoroutine(coroutine);
                }
            }
        }
        else if (!hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("I love collecting gems! Please bring me " + itemsNeeded + " gems.");
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
            playerInRange = false;
            TXT_input.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
