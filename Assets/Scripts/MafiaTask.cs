using UnityEngine;

public class MafiaTask : MonoBehaviour
{

    [SerializeField] private EnterParty manager;
    [SerializeField] private PartyTalk deer;

    public string result = "";

    private void Update()
    {

        if (deer.isInRange && Input.GetKeyDown(KeyCode.E) && deer.specialGem && deer.talkStep >= 3)
        {
            if (manager.acceptPolice)
            {
                deer.talkStep = 5;
                manager.helpedPolice = true;
            }
            if (!manager.acceptPolice) deer.talkStep = 4;
        }
    }
}
