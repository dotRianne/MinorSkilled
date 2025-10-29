using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Bird : MonoBehaviour
{
    public WorldLocation locationManager;
    public CollectiblesManager collectManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;

    [HideInInspector] public bool satisfyTask = false;
    private bool paidReward = false;
    private bool playerInRange = false;
    private int talkStep = 0;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && !paidReward && Input.GetKeyDown(KeyCode.E))
        {
            if (satisfyTask)
            {
                paidReward = true;
                lightBeam.SetActive(false);
                TXT_notif.SetText("Cool, thanks. I won't snitch on you no more.");
                StartCoroutine(coroutine);
            }
            else if(!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("I saw you destroying some vases earlier.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("I just so happen to know who's those were..");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("And he won't like hearing you destroyed his favorite one..");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("But.. I won't tell on you if you if you do something for me.");
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("I've lost my feed, find it and I'll keep quiet.");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("If you don't get me this feed I will tell him!");
                        TXT_input.SetText("");
                        StartCoroutine(coroutine);
                        break;
                }
            }
        }
        else if (playerInRange && paidReward && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("Go on, leave me be. Or I may change my mind.");
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
            TXT_input.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
