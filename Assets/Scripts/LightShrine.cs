using TMPro;
using UnityEngine;

public class LightShrine : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private TMP_Text TXT_notif;

    [SerializeField] private GameObject lightObject;
    [SerializeField] private ShrineTask task;

    public bool shrineLit = false;
    private bool isInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInRange = true;
            if (!shrineLit) TXT_input.SetText("[F] Light the shrine.");
            else if (shrineLit) TXT_notif.SetText("Shrine lit.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
            TXT_input.SetText("");
            TXT_notif.SetText("");
        }
    }

    private void Update()
    {
        if (shrineLit) return;
        if (isInRange && Input.GetKeyDown(KeyCode.F) && !shrineLit)
        {
            shrineLit = true;
            lightObject.SetActive(true);
            TXT_input.SetText("");
            TXT_notif.SetText("Shrine lit.");
        }
    }

}
