using TMPro;
using UnityEngine;

public class ParkourTask : MonoBehaviour
{
    public bool taskDone = false;
    public bool isParkouring = false;
    [SerializeField] private Transform startSpot;
    [SerializeField] private Transform endSpot;
    [SerializeField] private EnterParty manager;
    [SerializeField] private PartyTalk npcTalk;
    [SerializeField] private GameObject npc;


    private void Update()
    {
        if(npcTalk.talkStep >= 4 && !isParkouring)
        {
            isParkouring = true;
            npc.transform.position = endSpot.position;
        }
        if(taskDone && isParkouring)
        {
            isParkouring = false;
            manager.completedSquirrel = true;
            npcTalk.talkStep = 5;
        }
    }
}
