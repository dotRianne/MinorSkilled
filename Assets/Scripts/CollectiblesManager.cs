using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_gems;
    [SerializeField] private TMP_Text TXT_bones;

    public int collectedGems;
    public int collectedBones;

    private IEnumerator clear;

    public void Trigger(string type)
    {
    }

    public void Increase(string type, int amount)
    {
        clear = ClearNotif(3f);
        switch (type)
        {
            case "gem":
                collectedGems += amount;
                TXT_notif.SetText("You collected a gem!");
                TXT_gems.SetText(collectedGems.ToString() + " gems");
                StartCoroutine(clear);
                break;
            case "bone":
                collectedBones += amount;
                TXT_notif.SetText("You collected a bone!");
                TXT_bones.SetText(collectedBones.ToString() + " bones");
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
                TXT_gems.SetText(collectedBones.ToString() + " bones");
                break;
        }
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }

}
