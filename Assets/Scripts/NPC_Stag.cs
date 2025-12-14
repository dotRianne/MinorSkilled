using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Stag : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private int itemsNeeded = 4;
    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool satisfyTask = false;
    private bool hasRewarded = false;
    private bool playerInRange = false;

    private int talkStep = 0;
    private bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasRewarded && !satisfyTask && playerInRange)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("I want to get something matching for my friends");
                    TXT_input.SetText("[E] Continue talking");
                    talkStep++;
                    break;
                case 1:
                    TXT_notif.SetText("I was thinking something subtle but unique. like gems!");
                    TXT_input.SetText("[E] Continue talking");
                    talkStep++;
                    break;
                case 2:
                    TXT_notif.SetText("Could you help me find " + itemsNeeded + " gems?");
                    TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                    talkStep++;
                    break;
                case 3:
                    if (collectManager.collectedGems < itemsNeeded)
                    {
                        TXT_notif.SetText("Did you get " + itemsNeeded + " gems?");
                    }
                    else
                    {
                        talked = true;
                        satisfyTask = true;
                        hasRewarded = true;
                        collectManager.Decrease("gem", itemsNeeded);
                        TXT_notif.SetText("Thanks. My friends will love these.");
                        TXT_input.SetText("");
                        StartCoroutine(coroutine);
                        lightBeam.SetActive(false);
                        reward.SetActive(true);
                        locationManager.helpedDeer = true;
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !satisfyTask)
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
