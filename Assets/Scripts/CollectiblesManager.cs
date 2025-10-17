using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_gems;
    [SerializeField] private TMP_Text TXT_bones;
    [SerializeField] private TMP_Text TXT_marbles;

    public int collectedGems;
    public int collectedBones;
    public int collectedMarbles;

    private IEnumerator clear;

    public void Increase(string type, int amount)
    {
        clear = ClearNotif(3f);
        switch (type)
        {
            case "gem":
                collectedGems += amount;
                TXT_notif.SetText("You collected a gem!");
                TXT_gems.SetText(collectedGems.ToString() + " gems");
                StopCoroutine(clear);
                StartCoroutine(clear);
                break;
            case "bone":
                collectedBones += amount;
                TXT_notif.SetText("You collected a bone!");
                TXT_bones.SetText(collectedBones.ToString() + " bones");
                StopCoroutine(clear);
                StartCoroutine(clear);
                break;
            case "marbles":
                collectedMarbles += amount;
                TXT_notif.SetText("You collected a marble!");
                TXT_marbles.SetText(collectedMarbles.ToString() + " / 22");
                StopCoroutine(clear);
                StartCoroutine(clear);
                break;
        }
    }
    public void Decrease(string type, int amount)
    {
        switch (type)
        {
            case "gem":
                collectedGems -= amount;
                TXT_gems.SetText(collectedGems.ToString() + " gems");
                break;
            case "bone":
                collectedBones -= amount;
                TXT_bones.SetText(collectedBones.ToString() + " bones");
                break;
            case "marbles":
                collectedMarbles -= amount;
                TXT_marbles.SetText(collectedMarbles.ToString() + " / 22");
                break;
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }

}
