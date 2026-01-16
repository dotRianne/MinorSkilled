using System.Collections;
using TMPro;
using UnityEngine;

public class SpecialGem : MonoBehaviour
{
    [SerializeField] private PartyTalk npcTalk;
    [SerializeField] private GameObject visuals;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_input;

    private IEnumerator coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            npcTalk.specialGem = true;
            visuals.SetActive(false);
            TXT_notif.SetText("What an odd looking Gem.\nMaybe it serves another purpose?");
            coroutine = ClearNotif(3f);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }
}
