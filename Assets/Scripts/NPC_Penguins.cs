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
    [SerializeField] private GameObject rico;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool satisfyTask = false;
    private bool brokeAllVases = false;
    private bool playerInRange = false;
    public int brokenVases = 0;

    public int talkStep = 0;
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (brokenVases >= 8 && !brokeAllVases) brokeAllVases = true;

        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if(talkStep > 2)
            {
                if (!brokeAllVases)
                {
                    TXT_notif.SetText("Kowalski: There still appear to be some vases, sir.");
                    TXT_input.SetText("");
                }
                else if (brokeAllVases)
                {
                    switch (talkStep)
                    {
                        case 3:
                            TXT_notif.SetText("Skipper: Kowalski? Analysis!");
                            TXT_input.SetText("[E] Continue talk");
                            talkStep++;
                            break;
                        case 4:
                            TXT_notif.SetText("Kowalski: Vases destroyed, sir.");
                            TXT_input.SetText("[E] Continue talk");
                            talkStep++;
                            break;
                        case 5:
                            rico.SetActive(true);
                            TXT_notif.SetText("Rico: Kaboom?");
                            TXT_input.SetText("[E] Continue talk");
                            talkStep++;
                            break;
                        case 6:
                            talked = true;
                            satisfyTask = true;
                            TXT_notif.SetText("Skipper: No, Rico.. no kaboom.\nYet...");
                            TXT_input.SetText("");
                            StartCoroutine(coroutine);
                            lightBeam.SetActive(false);
                            reward.SetActive(true);
                            locationManager.helpedPenguins = true;
                            break;
                    }
                }
            }
            else if(talkStep < 3)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Skipper: Kowalski? Analysis!");
                        TXT_input.SetText("[E] Continue talk");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("Kowalski: These vases appear quite suspicous, sir.");
                        TXT_input.SetText("[E] Continue talk");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("Skipper: Then we must get apprehend them!");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            if(!talked) TXT_input.SetText("[E] Talk to " + npcInfo.charName);
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
