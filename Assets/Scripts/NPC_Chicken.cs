using System.Collections;
using TMPro;
using UnityEditor.TerrainTools;
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
    private bool hasTalked = false;

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
                TXT_notif.SetText("Thanks for helping, have this!");
                StartCoroutine(coroutine);
            }
            else if (!satisfyTask)
            {
                if (!hasTalked)
                {
                    TXT_notif.SetText("I lost my keys.. Can you find a way in to open the door for me?");
                    StartCoroutine(coroutine);
                    hasTalked = true;
                }
                else if(hasTalked && !openedDoor)
                {
                    TXT_notif.SetText("I just need you to find a way in and open the door..");
                    StartCoroutine(coroutine);
                }
                else if (hasTalked && openedDoor)
                {
                    TXT_notif.SetText("Thanks! meet me inside for a reward!");
                    StartCoroutine(coroutine);
                    visuals.SetActive(false);
                    transform.position = tpPosition.position;
                    transform.rotation = tpPosition.rotation;
                    visuals.SetActive(true);
                    playerInRange = false;
                    TXT_input.SetText("");
                    satisfyTask = true;
                }
            }
        }
        else if (playerInRange && paidReward && Input.GetKeyDown(KeyCode.E))
        {
            TXT_notif.SetText("Go on, leave me be. Or I may change my mind.");
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
