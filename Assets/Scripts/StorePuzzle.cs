using TMPro;
using UnityEngine;

public class StorePuzzle : MonoBehaviour
{
    [SerializeField] private NPC_Fish fish;
    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_input;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            fish.inStoreRange = true;
            TXT_input.SetText("[E] Grab supplies");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fish.inStoreRange = false;
            TXT_notif.SetText("");
            TXT_input.SetText("");
        }
    }

}
