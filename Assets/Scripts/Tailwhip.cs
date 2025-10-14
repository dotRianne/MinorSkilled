using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Tailwhip : MonoBehaviour
{
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private AudioSource audioSource;
    private bool isInRange = false;
    private bool isWhippable = true;

    [Header("Settings")]
    public bool showText = true;
    public bool destroyOnInteract = false;
    public bool moveOnInteract = false;
    public bool playSound = false;
    public AudioClip soundClip;

    [Header("break Settings")]
    public bool toggleEnabled;
    public GameObject disableObj;
    public GameObject enableObj;

    [Header("knock Settings")]
    public bool hasFutureFunctionality;
    public Transform endPosition;
    public MovableBox boxScript;
    public BoxTrigger boxTrigger1;
    public BoxTrigger boxTrigger2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(soundClip != null ) audioSource.clip = soundClip;
    }

    private void Update()
    {
        if (!isWhippable) return;
        if (destroyOnInteract)
        {
            if (isInRange && Input.GetMouseButtonDown(1))
            {
                isWhippable = false;
                TXT_input.SetText("");
                if (playSound) audioSource.Play();
                if (toggleEnabled)
                {
                    BoxCollider collider = this.GetComponent<BoxCollider>();
                    collider.enabled = false;
                    enableObj.SetActive(true);
                    disableObj.SetActive(false);
                }
                if (!toggleEnabled)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (moveOnInteract)
        {
            if (isInRange && Input.GetMouseButtonDown(1))
            {
                TXT_input.SetText("");
                if (playSound) audioSource.Play();
                transform.position = endPosition.position;
                if (hasFutureFunctionality)
                {
                    boxScript.canPull = true;
                    boxTrigger1.canPull = true;
                    boxTrigger2.canPull = true;
                }
                isWhippable = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isWhippable) return;
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
            if (showText) TXT_input.SetText("[Mouse2] Whip your tail.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isWhippable) return;
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            TXT_input.SetText("");
        }
    }
}
