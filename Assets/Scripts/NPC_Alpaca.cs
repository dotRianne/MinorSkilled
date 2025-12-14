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
    public GameObject stripes;

    [HideInInspector] public bool satisfyTask = false;
    [HideInInspector] public bool paidReward = false;
    private bool playerInRange = false;

    private int talkStep = 0;
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !paidReward && playerInRange)
        {
            talked = true;
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("That stupid dog...");
                        TXT_input.SetText("[E] Talk to " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("Oh.. sorry... Its just..\nHe eats so much!!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("I need to get more cooking supplies.. for the party!!!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("Could you help me get them?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("Make sure to place them in the box to the left!");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("We're still missing some stuff.");
                        TXT_input.SetText("");
                        break;
                }
            }
            else if (satisfyTask)
            {
                talked = true;
                paidReward = true;
                TXT_notif.SetText("Thanks. Now to hope he doesnt ruin this too...");
                TXT_input.SetText("");
                StopCoroutine(coroutine);
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
                locationManager.helpedAlpaca = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !paidReward)
        {
            playerInRange = true;
            if(!talked) TXT_input.SetText("[E] Talk to " + npcInfo.charName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
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