using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Jake : MonoBehaviour
{
    public WorldLocation locationManager;
    public CollectiblesManager collectManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;

    private bool satisfyTask = false;
    private bool playerInRange = false;
    public bool scoredGoal = false;

    private int talkStep = 0;
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !scoredGoal && !satisfyTask)
        {
            if (playerInRange)
            {
                talked = true;
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Do you want to play football with me?");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("Try scoring against me!");
                        TXT_input.SetText("[E] Challenge " + npcInfo.charName + "!");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.E) && scoredGoal && !satisfyTask && playerInRange)
        {
            talked = true;
            satisfyTask = true;
            locationManager.helpedFox = true;
            TXT_notif.SetText("You got skills! Lets play again soon!");
            TXT_input.SetText("");
            lightBeam.SetActive(false);
            reward.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            if (!talked)
            {
                TXT_input.SetText("[E] Talk to " + npcInfo.charName);
            }
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
