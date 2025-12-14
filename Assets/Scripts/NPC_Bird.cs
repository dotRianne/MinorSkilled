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
                locationManager.helpedBird = true;
                lightBeam.SetActive(false);
                TXT_notif.SetText("Cool, thanks. I won't snitch on you no more.");
                StartCoroutine(coroutine);
            }
            else if(!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("I saw you with those penguins earlier..");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("You broke them all!\nMy friend made those vases you know!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("You're going to do something for me to make up for this.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("I need you to find something for me..\nI lost my feed somewhere in this building.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("Retreive it for me and I'll consider you forgiven.");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("I'll make you regret it if you don't do as I say!");
                        TXT_input.SetText("");
                        StartCoroutine(coroutine);
                        break;
                }
            }
        }
        else if (playerInRange && paidReward && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("Good job. Now, leave me be.\nmay we meet again in a friendlier setting.");
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
