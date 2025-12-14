using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Chicken : MonoBehaviour
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
    [HideInInspector] public bool openedDoor = false;
    private bool paidReward = false;
    private bool playerInRange = false;

    private int talkStep = 0;
    public bool talked = false;

    [SerializeField] private Transform tpPosition;
    [SerializeField] GameObject visuals;

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
                reward.SetActive(true);
                TXT_notif.SetText("Thanks for helping me there... That was scary.");
                TXT_input.SetText("");
                locationManager.helpedChicken = true;
            }
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Ughh I'm such a klutz!");
                        TXT_input.SetText("[E] Whats up?");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("I forgot my keys in the house!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("Can you help me get back inside?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("I need you to find a way inside and open the door for me.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("Just ehh.. Ignore anything you see");
                        TXT_input.SetText("[E] Okayy..?");
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("The place is a mess, I don't think you will find my keys.");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 6:
                        TXT_notif.SetText("If you can just push the button to open the door that's enough.");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 7:
                        talked = true;
                        TXT_notif.SetText("");
                        TXT_input.SetText("");
                        break;
                }
                if(talked && !openedDoor)
                {
                    TXT_notif.SetText("Can you find a way inside?");
                    TXT_input.SetText("");
                }
                if(talked && openedDoor)
                {
                    TXT_notif.SetText("Thanks! Let me just grab my keys now. Meet me inside?");
                    TXT_input.SetText("");
                    StartCoroutine(coroutine);
                    visuals.SetActive(false);
                    transform.position = tpPosition.position;
                    transform.rotation = tpPosition.rotation;
                    visuals.SetActive(true);
                    satisfyTask = true;
                    playerInRange = false;
                }
            }
        }
        else if (playerInRange && paidReward && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("Thanks for helping me there... That was scary.");
            TXT_input.SetText("");
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
            if(openedDoor && satisfyTask && !paidReward)
            {
                TXT_notif.SetText("Thanks! Let me just grab my keys now. Meet me inside?");
                TXT_input.SetText("");
            }
            else
            {
                TXT_input.SetText("");
                TXT_notif.SetText("");
            }
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
