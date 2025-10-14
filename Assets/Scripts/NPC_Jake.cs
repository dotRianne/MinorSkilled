using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_Jake : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public NPCinfo npcInfo;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;
    [SerializeField] private GameObject lightBeam;

    private bool satisfyTask = false;
    private bool playerInRange = false;
    public bool scoredGoal = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && scoredGoal && !satisfyTask)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                satisfyTask = true;
                collectManager.Increase("bone", 1);
                TXT_notif.SetText("Wow that was amazing! Have this bone!");
                TXT_input.SetText("");
                lightBeam.SetActive(false);
                StartCoroutine(coroutine);
            }
        }
        else if (playerInRange && !scoredGoal && !satisfyTask)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TXT_notif.SetText("Can you score against me? I'll give you a reward!");
                StartCoroutine(coroutine);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            TXT_input.SetText("[E] Talk to " + npcInfo.charName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            TXT_input.SetText("");
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
