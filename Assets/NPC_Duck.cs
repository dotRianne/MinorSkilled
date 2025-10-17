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

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && !hasRewarded && Input.GetKeyDown(KeyCode.E))
        {
            if (satisfyTask)
            {
                hasRewarded = true;
                locationManager.helpedDuck = true;
                TXT_notif.SetText("Thanks for the help, have this.");
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
            }
            else if (!satisfyTask)
            {
                TXT_notif.SetText("I'm so exhausted, could you clean up the tennis balls for me?");
                StartCoroutine(coroutine);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasRewarded)
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
