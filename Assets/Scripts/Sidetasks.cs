using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Sidetasks : MonoBehaviour
{
    private IEnumerator coroutine;

    [Header("General Textfields")]
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_story;

    [Header("Score A Goal")]
    public NPC_Jake npcJake;
    private bool scoredAGoal = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball" && !scoredAGoal)
        {
            scoredAGoal = true;
            npcJake.scoredGoal = true;
            TXT_notif.SetText("Goal!");
            coroutine = ClearNotif(2f);
        }
    }

    IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
