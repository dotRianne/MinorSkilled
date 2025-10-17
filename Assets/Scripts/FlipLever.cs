using TMPro;
using UnityEngine;

public class FlipLever : MonoBehaviour
{
    private bool isInRange = false;

    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private ConveyorPath path;

    private void Update()
    {
        if (isInRange && Input.GetKeyUp(KeyCode.E))
        {
            path.Toggle();
            if (path.isActive)
            {
                path.leverOn.SetActive(true);
                path.leverOff.SetActive(false);
            }
            else if (!path.isActive)
            {
                path.leverOn.SetActive(false);
                path.leverOff.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
            TXT_input.SetText("[E] Flip lever.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            TXT_input.SetText("");
        }
    }

}
