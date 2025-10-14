using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarehouseLevel : MonoBehaviour
{
    [SerializeField] private GameObject unsuccessfulUI;
    [SerializeField] private GameObject successfulUI;
    [SerializeField] private GameObject leverUI;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private ThirdPersonController playerMovement;
    [SerializeField] private ConveyorPath conveyorLeft;
    [SerializeField] private GameObject leverLeftOn;
    [SerializeField] private GameObject leverLeftOff;
    [SerializeField] private ConveyorPath conveyorRight;
    [SerializeField] private GameObject leverRightOn;
    [SerializeField] private GameObject leverRightOff;
    private int marbleCount = 0;
    private int marbleMinimum = 18;
    private int marbleTotal = 22;

    private bool _nearLeverLeft = false;
    private bool _nearLeverRight = false;


    private void Start()
    {
        scoreText.SetText(marbleCount.ToString() + " / " + marbleTotal.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "marble":
                Destroy(other.gameObject);
                marbleCount++;
                scoreText.SetText(marbleCount.ToString() + " / " + marbleTotal.ToString());
                if (marbleCount > marbleMinimum) notificationText.SetText("You have collected enough marbles");
                break;
            case "door":
                if (marbleCount < marbleMinimum)
                {
                    unsuccessfulUI.SetActive(true);
                    playerMovement.canMove = false;
                }
                if (marbleCount >= marbleMinimum)
                {
                    successfulUI.SetActive(true);
                    playerMovement.canMove = false;
                }
                break;
            case "leverLeft":
                leverUI.SetActive(true);
                _nearLeverLeft = true;
                break;
            case "leverRight":
                leverUI.SetActive(true);
                _nearLeverRight = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "leverLeft":
                leverUI.SetActive(false);
                _nearLeverLeft = false;
                break;
            case "leverRight":
                leverUI.SetActive(false);
                _nearLeverRight = false;
                break;
        }
    }

    public void LeaveWarehouse_notdone()
    {
        PlayerPrefs.SetString("warehouse", "not_done");
        PlayerPrefs.Save();
        SceneManager.LoadScene("main");
    }
    public void LeaveWarehouse_done()
    {
        PlayerPrefs.SetString("warehouse", "done");
        PlayerPrefs.Save();
        SceneManager.LoadScene("main");
    }

    public void ReturnToLevel()
    {
        unsuccessfulUI.SetActive(false);
        successfulUI.SetActive(false);
        playerMovement.canMove = true;
    }

    // Example: call this when player interacts
    public void OnInteract(string side)
    {
        if (side == "left")
        {
            conveyorLeft.Toggle();
            if (conveyorLeft.isActive)
            {
                leverLeftOn.SetActive(true);
                leverLeftOff.SetActive(false);
            }
            else
            {
                leverLeftOn.SetActive(false);
                leverLeftOff.SetActive(true);
            }
            Debug.Log("ConveyorLeft is now " + (conveyorLeft.isActive ? "ON" : "OFF"));
        }
        if(side == "right")
        {
            conveyorRight.Toggle();
            if (conveyorRight.isActive)
            {
                leverRightOn.SetActive(true);
                leverRightOff.SetActive(false);
            }
            else
            {
                leverRightOn.SetActive(false);
                leverRightOff.SetActive(true);
            }
            Debug.Log("ConveyorRight is now " + (conveyorRight.isActive ? "ON" : "OFF"));
        }
    }

    private void Update()
    {
        if (_nearLeverLeft)
        {
            if (Input.GetKeyDown(KeyCode.E)) OnInteract("left");
        }
        if (_nearLeverRight)
        {
            if (Input.GetKeyDown(KeyCode.E)) OnInteract("right");
        }
    }
}
