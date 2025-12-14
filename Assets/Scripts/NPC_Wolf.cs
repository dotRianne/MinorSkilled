using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Wolf : MonoBehaviour
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
    public bool hasGivenBall = false;

    private int talkStep = 0;
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && !hasGivenBall && Input.GetKeyDown(KeyCode.E))
        {
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Hey you! I need your help!");
                        TXT_input.SetText("[E] Hey what's up.");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("My ball ended up on the balcony there..");
                        TXT_input.SetText("[E] How did it end up there?");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("I was playing with my friend jake..");
                        TXT_input.SetText("[E] Continue..");
                        talkStep++;
                        break;
                    case 3:
                        TXT_notif.SetText("Then this stupid dog came along and kicked the ball away!");
                        TXT_input.SetText("[E] Continue");
                        talkStep++;
                        break;
                    case 4:
                        TXT_notif.SetText("Can you please help me get it back?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 5:
                        TXT_notif.SetText("");
                        TXT_input.SetText("");
                        talkStep++;
                        talked = true;
                        break;
                }
            }
            else if (satisfyTask)
            {
                talked = true;
                hasGivenBall = true;
                locationManager.helpedWolf = true;
                TXT_notif.SetText("Thanks so much. Now we can play football at the party!");
                TXT_input.SetText("");
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            if(!talked) TXT_input.SetText("[E] Talk to " + npcInfo.charName);
            else if (talked && satisfyTask) TXT_notif.SetText("Thanks again. See ya at the party?");
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
