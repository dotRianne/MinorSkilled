using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [Header("Task Settings")]
    [Tooltip("Type the task name here.")]
    public string taskName;

    [Header("Optional References")]
    public GameObject targetObject; // For actions that need a target (like enabling/disabling)
    public GameObject lightOn;
    public GameObject lightOff;
    [Tooltip("Is the light on?")]
    public bool startLightOn = false;
    public TMP_Text TXT_input;
    public TMP_Text TXT_notif;

    private bool playerInside = false;
    private IEnumerator coroutine;

    // OpenGarage variables
    [Header("OpenGarage variables")]
    public float duration = 3f;
    private bool openingGarage = false;
    private bool garageIsOpen = false;
    private float elapsedTime = 0f;
    private Quaternion startRotation;
    private Quaternion endRotation;

    // OpenDoor variables
    [Header("OpenDoor variables")]
    [SerializeField] private NPC_Chicken emil;
    private bool doorIsOpen = false;


    // OpenDoor variables
    [Header("ElevatorUp variables")]
    public float elevatorDuration = 3f;
    private float elevatorElapsed = 0f;
    [SerializeField] private int distanceUp = 0;
    private bool elevatorIsUp = false;
    private bool elevatorMoving = false;

    private void Start()
    {
        if(lightOff != null && lightOn != null)
        {
            if (startLightOn)
            {
                lightOn.SetActive(true);
                lightOff.SetActive(false);
            }
            else if (!startLightOn)
            {
                lightOn.SetActive(false);
                lightOff.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            PerformTask();
        }

        // OpenGarage functionality
        if (openingGarage)
        {
            elapsedTime += Time.deltaTime;
            float time = Mathf.Clamp01(elapsedTime / duration);
            targetObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            if (elapsedTime >= duration)
            {
                openingGarage = false;
                garageIsOpen = true;
                if (lightOff != null && lightOn != null)
                {
                    lightOn.SetActive(false);
                    lightOff.SetActive(true);
                }
            }
        }

        if (elevatorMoving)
        {
            elevatorElapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elevatorElapsed / elevatorDuration);
            targetObject.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0, distanceUp, 0), time);
            if(elevatorElapsed >= elevatorDuration)
            {
                elevatorMoving = false;
                elevatorIsUp = true;
                if (lightOff != null && lightOn != null)
                {
                    lightOn.SetActive(false);
                    lightOff.SetActive(true);
                }
            }
        }
    }

    public void ResetElevator()
    {
        elevatorElapsed = 0;
        targetObject.transform.localPosition = Vector3.zero;
        elevatorIsUp = false;
    }

    private IEnumerator ClearNotif(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TXT_notif.SetText("");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TXT_input.SetText("[E] press button");
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TXT_input.SetText("");
            playerInside = false;
        }
    }

    private void PerformTask()
    {
        switch (taskName)
        {
            case "OpenGarage":
                if(garageIsOpen == true)
                {
                    TXT_notif.SetText("This door is already open.");
                    TXT_input.SetText("");
                    coroutine = ClearNotif(3f);
                    StartCoroutine(coroutine);
                    break;
                }
                if (targetObject != null)
                {
                    startRotation = targetObject.transform.rotation;
                    endRotation = Quaternion.Euler(0, 0, 90);
                    openingGarage = true;
                    TXT_input.SetText("");
                    if(lightOff != null && lightOn != null)
                    {
                        lightOff.SetActive(false);
                        lightOn.SetActive(true);
                    }
                }
                break;
            case "OpenDoor":
                if (doorIsOpen == false)
                {
                    targetObject.transform.rotation = Quaternion.Euler(-90, 0, 15);
                    doorIsOpen = true;
                    TXT_input.SetText("");
                    playerInside = false;
                    emil.openedDoor = true;
                }
                break;
            case "ElevatorUp":
                if (elevatorIsUp == true)
                {
                    TXT_notif.SetText("The elevator is already up.");
                    TXT_input.SetText("");
                    coroutine = ClearNotif(3f);
                    StartCoroutine(coroutine);
                    break;
                }
                if (targetObject != null)
                {
                    elevatorMoving = true;
                    TXT_input.SetText("");
                    if (lightOff != null && lightOn != null)
                    {
                        lightOff.SetActive(false);
                        lightOn.SetActive(true);
                    }
                }
                break;
            default:
                Debug.LogWarning("Unknown task: " + taskName);
                break;
        }
    }
}