using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Squirrel : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [Header("References")]
    public GameObject player;
    public CharacterController playerCC;
    public GameObject squirrelSpot;
    public GameObject levelStart;
    public GameObject levelEnd;
    public GameObject levelHUD;
    public GameObject mainHUD;
    public GameObject lightBeam;
    public GameObject reward;
    public RaceManager raceManager;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    private bool paidTask = false;
    public bool satisfyTask = false;
    private bool playerInRange = false;
    private int talkStep = 0;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !satisfyTask)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("Hey! hey! hey you! do you have what it takes to go fast?");
                    TXT_input.SetText("[E] Continue talking");
                    talkStep++;
                    break;
                case 1:
                    TXT_notif.SetText("I just KNOW you can't beat me in a race!");
                    TXT_input.SetText("[E] Continue talking");
                    talkStep++;
                    break;
                case 2:
                    TXT_notif.SetText("Want to try me anyways?! Lets go!");
                    TXT_input.SetText("[E] Agree to race against " + npcInfo.name);
                    talkStep++;
                    break;
                case 3:
                    TpToRace();
                    break;
            }
        }
        if (satisfyTask && !paidTask)
        {
            paidTask = true;
            reward.SetActive(true);
            lightBeam.SetActive(false);
            locationManager.helpedSquirrel = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            TXT_input.SetText("[E] Talk to " + npcInfo.charName);
        }
        else if (other.gameObject.tag == "Player" && satisfyTask)
        {
            TXT_notif.SetText("Good race!!");
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

    public void TpToRace()
    {
        // Teleport player to new area
        playerCC.enabled = false;
        player.transform.position = levelStart.transform.position;
        player.transform.rotation = levelStart.transform.rotation;
        playerCC.enabled = true;

        // Toggle HUDs
        mainHUD.SetActive(false);
        levelHUD.SetActive(true);

        // Toggle settings
        raceManager.atRaceArea = true;
        raceManager.ResetSettings();
    }

    public void TpToSquirrel()
    {
        // Teleport player to new area
        playerCC.enabled = false;
        player.transform.position = squirrelSpot.transform.position;
        player.transform.rotation = squirrelSpot.transform.rotation;
        playerCC.enabled = true;

        // Toggle HUDs
        mainHUD.SetActive(true);
        levelHUD.SetActive(false);
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
