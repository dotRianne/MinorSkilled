using System.Collections;
using TMPro;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_gems;
    [SerializeField] private TMP_Text TXT_tokens;
    [SerializeField] private TMP_Text TXT_marbles;

    public int collectedGems;
    public int collecedTokens;
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
            case "token":
                collecedTokens += amount;
                TXT_notif.SetText("You collected a token!");
                TXT_tokens.SetText(collecedTokens.ToString() + " tokens");
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
            case "token":
                collecedTokens -= amount;
                TXT_tokens.SetText(collecedTokens.ToString() + " tokens");
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
