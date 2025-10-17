using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Alpaca : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;

    [HideInInspector] public bool satisfyTask = false;
    [HideInInspector] public bool paidReward = false;
    private bool playerInRange = false;
    private bool hasTalked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E) && !paidReward)
        {
            if (satisfyTask)
            {
                paidReward = true;
                TXT_notif.SetText("Thanks! Have this for the effort.");
                TXT_input.SetText("");
                StopCoroutine(coroutine);
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
                locationManager.helpedAlpaca = true;
            }
            else if (!satisfyTask)
            {
                TXT_notif.SetText("You're still missing some stuff. Make sure its in the crate!");
                StopCoroutine(coroutine);
                StartCoroutine(coroutine);
            }
            else Debug.Log("We got past all options somehow. check code for errors.");
        }
        else if (!hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            hasTalked = true;
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
            TXT_notif.SetText("Could you help me get my groceries in the crate?");
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