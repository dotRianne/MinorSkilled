using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_Berd : MonoBehaviour
{
    public CollectiblesManager collectManager;
    public WorldLocation locationManager;
    public NPCinfo npcInfo;

    [Header("References")]
    public GameObject player;
    public CharacterController playerCC;
    public GameObject warehousePos;
    public GameObject warehouseHUD;
    public GameObject mainHUD;
    public GameObject lightBeam;
    public GameObject reward;

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
            if(talkStep == 0)
            {
                TXT_notif.SetText("I need your help! Can you help find my marbles?");
                TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                talkStep++;
            }
            else if (talkStep == 1)
            {
                TpToWarehouse();
            }
        } 
        if(satisfyTask && !paidTask)
        {
            paidTask = true;
            reward.SetActive(true);
            lightBeam.SetActive(false);
            locationManager.helpedDog = true;
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
            TXT_notif.SetText("Thanks for returning my marbles!");
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

    private void TpToWarehouse()
    {
        // Teleport player to new area
        playerCC.enabled = false;
        player.transform.position = warehousePos.transform.position;
        player.transform.rotation = warehousePos.transform.rotation;
        playerCC.enabled = true;

        // Toggle HUDs
        mainHUD.SetActive(false);
        warehouseHUD.SetActive(true);
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
