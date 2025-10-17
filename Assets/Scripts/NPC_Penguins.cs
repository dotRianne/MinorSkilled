using NUnit.Framework;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NPC_Penguins : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool satisfyTask = false;
    private bool brokeAllVases = false;
    private bool playerInRange = false;
    private bool hasTalked = false;
    private int hasTalkedStep = 0;
    public int brokenVases = 0;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (brokenVases >= 8 && !brokeAllVases) brokeAllVases = true;
        if (hasTalked && playerInRange && !brokeAllVases)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                TXT_notif.SetText("Kowalski: There still appear to be some vases, sir.");
                TXT_input.SetText("");
            }
        }
            if (hasTalked && playerInRange && brokeAllVases && !satisfyTask)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(hasTalkedStep == 0)
                {
                    TXT_notif.SetText("Skipper: Kowalski? Analysis!");
                    TXT_input.SetText("[E] Continue talk");
                    hasTalkedStep = 1;
                }
                else if(hasTalkedStep == 1)
                {
                    satisfyTask = true;
                    collectManager.Increase("bone", 1);
                    TXT_notif.SetText("Kowalski: Vases destroyed, sir.");
                    TXT_input.SetText("");
                    StartCoroutine(coroutine);
                    lightBeam.SetActive(false);
                    reward.SetActive(true);
                    locationManager.helpedPenguins = true;
                }
            }
        }
        else if (!hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (hasTalkedStep == 0)
            {
                TXT_notif.SetText("Skipper: Kowalski? Analysis!");
                TXT_input.SetText("[E] Continue talk");
                hasTalkedStep = 1;
            }
            else if (hasTalkedStep == 1)
            {
                TXT_notif.SetText("Kowalski: These vases are in the way, sir.");
                TXT_input.SetText("[E] Continue talk");
                hasTalkedStep = 2;
            }
            else if (hasTalkedStep == 2)
            {
                TXT_notif.SetText("Skipper: Then we must get rid of them!");
                TXT_input.SetText("");
                hasTalked = true;
                hasTalkedStep = 0;
                StartCoroutine(coroutine);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
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
            TXT_notif.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
