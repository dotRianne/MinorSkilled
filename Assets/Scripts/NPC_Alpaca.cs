using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Alpaca : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;

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
                collectManager.Increase("bone", 1);
                TXT_notif.SetText("Thanks! You should head to the construction site. Someone there will help you further.");
                TXT_input.SetText("");
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
            }
            else if (!satisfyTask)
            {
                TXT_notif.SetText("You havent collected everything for me yet. I need a tomato, buns, lettuce, a carrot and potatoes.");
                StartCoroutine(coroutine);
            }
            else Debug.Log("We got past all options somehow. check code for errors.");
        }
        else if (!hasTalked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            hasTalked = true;
            StartCoroutine(coroutine);
            TXT_notif.SetText("I can guide you, but first do my groceries. I need a tomato, buns, lettuce, a carrot and potatoes.");
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
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
