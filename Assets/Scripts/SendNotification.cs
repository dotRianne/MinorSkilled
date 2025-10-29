using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SendNotification : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_extra;
    [SerializeField] private WorldLocation manager;
    public string notification;

    [SerializeField] private bool isMarketBarrier;
    [SerializeField] private bool isConstructionBarrier;
    [SerializeField] private bool isSanctuaryBarrier;

    private IEnumerator coroutine;

    public void SendNotif()
    {
        string notif = "";
        if (isMarketBarrier)
        {
            if (!manager.helpedFox || !manager.helpedDuck || !manager.helpedDeer) notif += "You still need to help: ";
            if (!manager.helpedFox) notif += "Jake the fox, ";
            if (!manager.helpedDuck) notif += "Kwek the duck, ";
            if (!manager.helpedFox) notif += "Meryl the deer.";
        }
        if (isConstructionBarrier)
        {
            if (!manager.helpedDog || !manager.helpedWolf || !manager.helpedPenguins || !manager.helpedAlpaca) notif += "You still need to help: ";
            if (!manager.helpedDog) notif += "Berd the dog, ";
            if (!manager.helpedWolf) notif += "Bobo the wolf, ";
            if (!manager.helpedPenguins) notif += "the penguins, ";
            if (!manager.helpedAlpaca) notif += "Floris the alpaca.";
        }
        if (isSanctuaryBarrier)
        {
            if (!manager.helpedBird || !manager.helpedSquirrel || !manager.helpedFish || !manager.helpedChicken) notif += "You still need to help: ";
            if (!manager.helpedBird) notif += "Pablo the bird, ";
            if (!manager.helpedSquirrel) notif += "Harry the squirrel, ";
            if (!manager.helpedFish) notif += "Glade the fish, ";
            if (!manager.helpedChicken) notif += "Emile the chicken.";
        }

        // Send notifs
        TXT_notif.SetText(notification);
        TXT_extra.SetText(notif);
    }
    public void ClearNotif()
    {
        TXT_notif.SetText("");
        TXT_extra.SetText("");
    }
    public void SendTimedNotif()
    {
        TXT_notif.SetText(notification);
        coroutine = ClearTimedNotif(3f);
        StartCoroutine(coroutine);
    }
    private IEnumerator ClearTimedNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SendNotif();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            ClearNotif();
    }
}
