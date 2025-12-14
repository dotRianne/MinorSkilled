using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Fish : MonoBehaviour
{
    public WorldLocation locationManager;
    public CollectiblesManager collectManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;

    [HideInInspector] public bool satisfyTask = false;
    private bool paidReward = false;
    private bool playerInRange = false;
    private int talkStep = 0;

    public bool inStoreRange = false;
    private bool obtainedItems = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if(inStoreRange && Input.GetKeyDown(KeyCode.E) && !obtainedItems)
        {
            obtainedItems = true;
            TXT_notif.SetText("You grabbed a shovel and bags.");
            TXT_input.SetText("");
            talkStep++;
        }


        if (playerInRange && !paidReward && Input.GetKeyDown(KeyCode.E))
        {
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Some lunatic broke my vases earlier!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("My friend said it was that stupid dog, Berd.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("I want to get back at him, but first I need to take care of this..");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("Do you think you could help me?");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("See that store over there?");
                        TXT_input.SetText("[E] Yeah?");
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("I need you to get me a shovel and some bags.");
                        TXT_input.SetText("[E] Why would you need those?");
                        talkStep++;
                        break;
                    case 6:
                        TXT_notif.SetText("I collect the materials for the vases myself.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 7:
                        TXT_notif.SetText("I'll be collecting sediment from the pond later.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 8:
                        TXT_notif.SetText("So.. Will you help me?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 9:
                        TXT_notif.SetText("");
                        TXT_input.SetText("");
                        break;
                    case 10:
                        satisfyTask = true;
                        break;
                }
            }
            if (satisfyTask)
            {
                paidReward = true;
                locationManager.helpedFish = true;
                reward.SetActive(true);
                lightBeam.SetActive(false);
                TXT_notif.SetText("Thanks. Maybe we'll see eachother at the party later.");
                TXT_input.SetText("");
                StartCoroutine(coroutine);
            }
        }
        else if (playerInRange && paidReward && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("Thanks. Maybe we'll see eachother at the party later.");
            TXT_input.SetText("");
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !paidReward)
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
            TXT_notif.SetText("");
            TXT_input.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
