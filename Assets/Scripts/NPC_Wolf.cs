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

    private IEnumerator coroutine;

    private void Start()
    {
        coroutine = ClearNotif(3f);
    }

    private void Update()
    {
        if (playerInRange && !hasGivenBall && Input.GetKeyDown(KeyCode.E))
        {
            if (satisfyTask)
            {
                hasGivenBall = true;
                locationManager.helpedWolf = true;
                TXT_notif.SetText("Thanks so much! have this bone!");
                StartCoroutine(coroutine);
                lightBeam.SetActive(false);
                reward.SetActive(true);
            }
            else if (!satisfyTask)
            {
                TXT_notif.SetText("I accidentally lost my ball up there... Can you get it back for me?");
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
