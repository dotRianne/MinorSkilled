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
    public bool talked = false;

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {
            talked = true;
            if (!satisfyTask)
            {
                switch (talkStep)
                {
                    case 0:
                        TXT_notif.SetText("Help! I've lost all my marbles in the warehouse..");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 1:
                        TXT_notif.SetText("Could you help me find them back?");
                        TXT_input.SetText("[E] Continue talking");
                        talkStep++;
                        break;
                    case 2:
                        TXT_notif.SetText("I can show you were I last saw them?");
                        TXT_input.SetText("[E] Agree to help " + npcInfo.charName);
                        talkStep++;
                        break;
                    case 3:
                        TpToWarehouse();
                        talkStep--;
                        break;
                }
            }
            else if (satisfyTask && !paidTask)
            {
                paidTask = true;
                reward.SetActive(true);
                lightBeam.SetActive(false);
                locationManager.helpedDog = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !satisfyTask)
        {
            playerInRange = true;
            if(!talked) TXT_input.SetText("[E] Talk to " + npcInfo.charName);
        }
        else if (other.gameObject.tag == "Player" && satisfyTask)
        {
            playerInRange = true;
            TXT_notif.SetText("Now I can play with them at the party!");
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
        playerInRange = false;
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
