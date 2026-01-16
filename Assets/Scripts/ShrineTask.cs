using TMPro;
using UnityEngine;

public class ShrineTask : MonoBehaviour
{
    public bool taskDone = false;
    [SerializeField] private GameObject scenery;
    [SerializeField] private EnterParty manager;
    [SerializeField] private PartyTalk npcTalk;

    [SerializeField] private LightShrine shrine1;
    [SerializeField] private LightShrine shrine2;
    [SerializeField] private LightShrine shrine3;
    [SerializeField] private LightShrine shrine4;

    private void Update()
    {
        if(shrine1.shrineLit && shrine2.shrineLit && shrine3.shrineLit && shrine4.shrineLit && !taskDone)
        {
            taskDone = true;
            scenery.SetActive(true);
            manager.completedChicken = true;
            npcTalk.talkStep = 7;
        }
    }
}
