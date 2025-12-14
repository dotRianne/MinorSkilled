using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : MonoBehaviour
{
    [Header("Race settings")]
    [SerializeField] private float goalTime;
    [SerializeField] private GameObject startBarrier;
    [SerializeField] private RaceInteract startBlock;
    [SerializeField] private RaceInteract endBlock;
    [SerializeField] private NPC_Squirrel npc;
    [SerializeField] private NPCinfo npcInfo;
    public bool raceStarting;
    public bool raceOngoing;
    public bool raceComplete;
    public bool atRaceArea;

    private float countdownTimer;
    private float currentGoalTime;
    private float raceTimer;

    private float playerPB;

    private int attempts;
    [SerializeField] private TMP_Text TXT_goal;
    [SerializeField] private TMP_Text TXT_timer;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_extra;

    // Puzzle references
    [SerializeField] private GameObject sc1_object;
    private Tailwhip sc1_script;
    [SerializeField] private GameObject sc1_visuals;

    [SerializeField] private ButtonAction sc2_object;

    [SerializeField] private GameObject sc3_topbox;
    [SerializeField] private GameObject sc3_downbox;
    [SerializeField] private GameObject sc3_dragbox;
    [SerializeField] private GameObject sc3_dragboxpos;
    private Tailwhip sc3_script;
    [SerializeField] private GameObject sc3_visuals;

    [SerializeField] private GameObject sc4_object1;
    [SerializeField] private GameObject sc4_object2;
    private Tailwhip sc4_script1;
    private Tailwhip sc4_script2;
    [SerializeField] private GameObject sc4_visuals1;
    [SerializeField] private GameObject sc4_visuals2;

    private IEnumerator coroutine;

    private void Start()
    {
        currentGoalTime = goalTime;
        sc1_script = sc1_object.GetComponent<Tailwhip>();
        sc3_script = sc3_topbox.GetComponent<Tailwhip>();
        sc4_script1 = sc4_object1.GetComponent<Tailwhip>();
        sc4_script2 = sc4_object2.GetComponent<Tailwhip>();

        coroutine = ClearNotif(3f);
        TXT_goal.SetText("goal: " + currentGoalTime.ToString());

        //playerPB = PlayerPrefs.GetFloat("playerBest", 0f);
    }

    private void Update()
    {
        if (!atRaceArea) return;

        if (startBlock.isInside && !raceOngoing && !raceStarting)
        {
            TXT_input.SetText("[E] Start the race.");
            if(Input.GetKeyDown(KeyCode.E))
            {
                raceStarting = true;
                TXT_input.SetText("");
                TXT_extra.SetText("[Q] Leave");
            }
        }
        else if(startBlock.isInside && !raceOngoing && raceStarting) 
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer > 2f) TXT_notif.SetText("Race starting in 3...");
            else if (countdownTimer > 1f) TXT_notif.SetText("Race starting in 2..");
            else if (countdownTimer > 0f) TXT_notif.SetText("Race starting in 1.");
            else if (countdownTimer <= 0f && countdownTimer >= -1f)
            {
                TXT_notif.SetText("");
                startBarrier.SetActive(false);
                raceOngoing = true;
                TXT_extra.SetText("[R] Reset | [Q] Leave");
            }
        }
        if (raceOngoing)
        {
            raceTimer += Time.deltaTime;
            TXT_timer.SetText("time: " + raceTimer.ToString("0"));
        }

        if(endBlock.isInside && raceOngoing)
        {
            raceOngoing = false;
            TXT_extra.SetText("");
            CheckResults();
        }
        KeyInputs();
    }

    private void CheckResults()
    {
        if (raceTimer < playerPB)
        {
            PlayerPrefs.SetFloat("playerBest", raceTimer);
            PlayerPrefs.Save();
        }

        if(raceTimer <= currentGoalTime)
        {
            if (raceTimer <= 100f) currentGoalTime = raceTimer-1;
            else if (raceTimer > 100f && raceTimer <= 110f) currentGoalTime = 100f;
            else if (raceTimer > 110f && raceTimer <= 120f) currentGoalTime = 110f;
            else if (raceTimer > 120f && raceTimer <= 130f) currentGoalTime = 120f;
            else if (raceTimer > 130f && raceTimer <= 140f) currentGoalTime = 130f;
            else if (raceTimer > 140f && raceTimer <= 150f) currentGoalTime = 140f;

            raceComplete = true;
            npc.satisfyTask = true;
            TXT_notif.SetText("You win!");
            TXT_input.SetText("[Q] Return to " + npcInfo.name);
            StartCoroutine(coroutine);
        }
        else if(raceTimer > currentGoalTime)
        {
            if(attempts >= 2)
            {
                if (raceTimer <= 160f) { }
                if (raceTimer > 160f && raceTimer <= 170f) currentGoalTime = 160f;
                else if (raceTimer > 170f && raceTimer <= 180f) currentGoalTime = 170f;
                else if (raceTimer > 180f && raceTimer <= 190f) currentGoalTime = 180f;
                else if (raceTimer > 190f && raceTimer <= 200f) currentGoalTime = 190f;
                else if (raceTimer > 200f && raceTimer <= 210f) currentGoalTime = 200f;
                else if (raceTimer > 210f && raceTimer <= 220f) currentGoalTime = 210f;
                else if (raceTimer > 220f && raceTimer <= 230f) currentGoalTime = 220f;
                else if (raceTimer > 230f && raceTimer <= 240f) currentGoalTime = 230f;
                else if (raceTimer > 240f && raceTimer <= 250f) currentGoalTime = 240f;
                else if (raceTimer > 250f) currentGoalTime = 250f;
            }
            TXT_notif.SetText("You Lose... Try again!");
            TXT_input.SetText("[R] Restart | [Q] Return to " + npcInfo.name);
        }
    }

    private void KeyInputs()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            npc.TpToSquirrel();
            atRaceArea = false;
            TXT_extra.SetText("");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (endBlock.isInside)
            {
                npc.player.transform.position = npc.levelEnd.transform.position;
            }
            startBlock.gameObject.SetActive(false);
            endBlock.gameObject.SetActive(false);
            npc.TpToRace();
        }
    }

    public void ResetPlayer()
    {
        npc.TpToSquirrel();
    }

    public void ResetSettings()
    {
        attempts += 1;
        TXT_goal.SetText("goal: " + currentGoalTime.ToString());
        countdownTimer = 3f;
        raceTimer = 0f;
        TXT_timer.SetText("time: 0");
        raceOngoing = false;
        raceStarting = false;
        startBarrier.SetActive(true);

        // Puzzle resets
        Debug.Log("Resetting puzzle 1.");
        sc1_object.SetActive(true);
        sc1_visuals.SetActive(true);
        sc1_script.isWhippable = true;
        Debug.Log("Puzzle 1 reset.");

        Debug.Log("Resetting puzzle 2.");
        sc2_object.ResetElevator();
        Debug.Log("Puzzle 2 reset.");

        Debug.Log("Resetting puzzle 3.");
        sc3_topbox.SetActive(true);
        sc3_visuals.SetActive(true);
        sc3_dragbox.transform.localPosition = sc3_dragboxpos.transform.localPosition;
        sc3_script.isWhippable = true;
        sc3_downbox.SetActive(false);
        Debug.Log("Puzzle 3 reset.");

        Debug.Log("Resetting puzzle 4.");
        sc4_object1.SetActive(true);
        sc4_object2.SetActive(true);
        sc4_script1.isWhippable = true;
        sc4_script2.isWhippable = true;
        sc4_visuals1.SetActive(true);
        sc4_visuals2.SetActive(true);
        Debug.Log("Puzzle 4 reset.");

        startBlock.gameObject.SetActive(true);
        endBlock.gameObject.SetActive(true);
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
