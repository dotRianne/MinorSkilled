using NUnit.Framework.Constraints;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Duck : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [SerializeField] private GameObject lightBeam;
    [SerializeField] private GameObject reward;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool playerInRange = false;
    public bool satisfyTask = false;
    public bool hasRewarded = false;

    private int talkStep = 0;
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && !hasRewarded && Input.GetKeyDown(KeyCode.E))
        {
            talked = true;
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Oh no.. this is a disaster!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("That damn fox left this place a mess!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("I have to leave but these tennis balls are everywhere!");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("Could you help me place them all back?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("Thanks! Just place them back in the holder.");
                        TXT_input.SetText("");
                        talkStep++;
                        break;
                }
            }
            else if (satisfyTask)
            {
                talked = true;
                hasRewarded = true;
                locationManager.helpedDuck = true;
                TXT_notif.SetText("Thanks for the help, see ya at the party!");
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasRewarded)
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
