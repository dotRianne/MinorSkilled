using TMPro;
using UnityEngine;
using System.Collections;

public class EnterParty : MonoBehaviour
{
    [SerializeField] private WorldLocation manager;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_extra;
    [SerializeField] private TMP_Text TXT_PartyTime;

    // Setting Toggles
    [SerializeField] private GameObject worldNPCs;
    [SerializeField] private GameObject parkNPCs;
    [SerializeField] private GameObject daylight;
    [SerializeField] private GameObject evelight;
    [SerializeField] private GameObject blackscreen;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject tpSpot;
    [SerializeField] private GameObject invisWalls;

    // Game Endings
    [SerializeField] private GameObject Ending_TimeUp; // 5 minute timer ran out. You get a message that the party ended, people went home and you are left with the mess.
    [SerializeField] private GameObject Ending_Mafia; // You joined the mafia and escaped the police. You guys left the party and left others with the mess.
    [SerializeField] private GameObject Ending_Police; // You helped the police infiltrate and take down the mafia. You went back to the station and left the others with the mess.

    /// <summary> How to perform actions
    /// How to help the police:
    /// - Find and talk to the police
    /// - Choose option 1 of 3. (Agree | Think about it | Refuse)
    /// 
    /// How to join the mafia:
    /// - Find a "special gem" in the park
    /// - Talk to Meryl the deer and "give the gem". (The deer doesn't prompt you to show her it. You just have to press E when near her)
    /// </summary>

    /// <summary> How to get certain endings
    /// The user is able to join the mafia at any moment by showing them a "special gem." The player is not prompted to do so, but it is possible.
    /// if the player gives the deer the special gem, they join the mafia.
    /// The player is able to help the police at any moment. When talking to the police the player can either "Accept", "Think about it" or "Refuse".
    ///
    /// Police ending can happen in 2 ways:
    /// 1. The player helps the police and then "joins the mafia" (Showing a special gem to the deer).
    /// 2. The player is in the mafia and agrees to help the police. The player has to talk to Jeff the fish.
    ///
    /// Mafia ending can happen in 1 way (with one edge-case):
    /// 1. The player has joined the mafia (showing a special gem to the deer).
    /// Edge case: The player agrees to help the police after joining the mafia but doesn't talk to the fish: The mafia ending still happens.
    ///
    /// The regular ending happens in 2 ways:
    /// 1. The 5 minute timer runs out without any other ending occuring.
    /// 2. The player agrees to help the police but doesn't join the mafia.
    /// </summary>


    [Header ("Party Settings")]
    public float partyTime = 20;

    [Header("Game State Booleans")]
    public bool foundGem = false;
    public bool joinedMafia = false;
    public bool acceptPolice = false;
    public bool helpedPolice = false;
    public bool completedSquirrel = false;
    public bool outOfTime = false;
    public bool playedEndCredits = false;

    public CharacterController cc;
    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(5f);
        cc = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!manager.atParty)
            {
                manager.atParty = true;
                TXT_notif.SetText("Welcome to the party!");
                TXT_extra.SetText("The party will last 3 minutes.");
                worldNPCs.SetActive(false);
                parkNPCs.SetActive(true);
                daylight.SetActive(false);
                evelight.SetActive(true);
                blackscreen.SetActive(true);

                cc.enabled = false;
                player.transform.position = tpSpot.transform.position;
                invisWalls.SetActive(true);
                cc.enabled = true;

                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
        if (blackscreen.activeSelf) blackscreen.SetActive(false);
        if (invisWalls.activeSelf) invisWalls.SetActive(false);
    }

    private void Update()
    {
        if (helpedPolice)
        {
            partyTime = 0;
            outOfTime = true;
            TXT_PartyTime.SetText("Time: 0");
        }
        if (manager.atParty)
        {
            if (partyTime > 0)
            {
                partyTime -= Time.deltaTime;
                TXT_PartyTime.SetText("Time: " + partyTime.ToString("0"));
            }
            else if (!outOfTime)
            {
                outOfTime = true;
                TXT_PartyTime.SetText("Time: 0");
            }
        }
        if (outOfTime && !playedEndCredits)
        {
            RunCredits();
        }
    }
    public void SpecialGem()
    {
        foundGem = true;
        TXT_notif.SetText("You picked up a gem that appears different.");
        StartCoroutine(coroutine);
    }

    public void AgreeToHelpPolice()
    {
        acceptPolice = true;
        if (joinedMafia) TXT_notif.SetText("Talk to Jeff to gather information.");
        else TXT_notif.SetText("Find a way to infiltrate the mafia.");
    }

    public void JoinTheMafia()
    {
        joinedMafia = true;
        if (acceptPolice) TXT_notif.SetText("FBI, OPEN UP!");
        else TXT_notif.SetText("Welcome to the mafia, bud.");
    }

    public void CatchTheMafia()
    {
        helpedPolice = true;
        RunCredits();
    }

    public void RunCredits()
    {
        playedEndCredits = true;
        if (!joinedMafia && !helpedPolice) Ending_TimeUp.SetActive(true);
        else if (joinedMafia && !helpedPolice) Ending_Mafia.SetActive(true);
        else if (helpedPolice) Ending_Police.SetActive(true);
    }
}
