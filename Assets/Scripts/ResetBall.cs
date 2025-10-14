using TMPro;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    private bool isInRange = false;
    public Transform startPos;
    public GameObject parent;
    public TMP_Text TXT_extra;
    private Rigidbody parentRb;

    private void Start()
    {
        startPos.position = parent.transform.position;
        parentRb = GetComponentInParent<Rigidbody>();
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.R))
        {
            parent.transform.position = startPos.position;
            parentRb.linearVelocity = Vector3.zero;
            parentRb.angularVelocity = Vector3.zero;
            Debug.Log("reset ball.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TXT_extra.SetText("[R] Reset ball position.");
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TXT_extra.SetText("");
            isInRange = false;
        }
    }
}
