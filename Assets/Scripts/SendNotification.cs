using System.Collections;
using TMPro;
using UnityEngine;

public class SendNotification : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_Notif;
    public string notification;

    private IEnumerator coroutine;

    public void SendNotif()
    {
        TXT_Notif.SetText(notification);
    }
    public void ClearNotif()
    {
        TXT_Notif.SetText("");
    }
    public void SendTimedNotif()
    {
        TXT_Notif.SetText(notification);
        coroutine = ClearTimedNotif(3f);
        StartCoroutine(coroutine);
    }
    private IEnumerator ClearTimedNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_Notif.SetText("");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            SendNotif();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            ClearNotif();
    }
}
