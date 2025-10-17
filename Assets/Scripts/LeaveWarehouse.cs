using TMPro;
using System.Collections;
using UnityEngine;

public class LeaveWarehouse : MonoBehaviour
{
    [SerializeField] private NPC_Berd berd;
    [SerializeField] private GameObject berdPos;
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterController playerCC;
    [SerializeField] private CollectiblesManager manager;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;

    private bool isInRange = false;
    private int msgState = 0;

    private IEnumerator clear;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInRange = true;
            TXT_input.SetText("[E] Leave the Warehouse.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            TXT_input.SetText("");
        }
    }

    private void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            clear = ClearNotif(3f);
            if (manager.collectedMarbles >= 18)
            {
                TXT_notif.SetText("You left the warehouse.");
                berd.satisfyTask = true;

                // Teleport player
                playerCC.enabled = false;
                player.transform.position = berdPos.transform.position;
                player.transform.rotation = berdPos.transform.rotation;
                playerCC.enabled = true;

                // Change HUDs
                berd.mainHUD.SetActive(true);
                berd.warehouseHUD.SetActive(false);

                // Clear notif
                StopCoroutine(clear);
                StartCoroutine(clear);
            }
            else if (manager.collectedMarbles < 18)
            {
                if (msgState == 0)
                {
                    TXT_notif.SetText("You haven't collected enough marbles. Are you sure you want to leave?");
                    StopCoroutine(clear);
                    StartCoroutine(clear);
                    msgState++;
                }
                else if(msgState == 1)
                {
                    TXT_notif.SetText("You left the Warehouse without completing the quest. You can return any time by talking to Berd again.");
                    msgState--;

                    // Teleport player
                    playerCC.enabled = false;
                    player.transform.position = berdPos.transform.position;
                    player.transform.rotation = berdPos.transform.rotation;
                    playerCC.enabled = true;

                    // Change HUDs
                    berd.mainHUD.SetActive(true);
                    berd.warehouseHUD.SetActive(false);

                    // Clear notif
                    StopCoroutine(clear);
                    StartCoroutine(clear);
                }
            }
        }
    }
    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
