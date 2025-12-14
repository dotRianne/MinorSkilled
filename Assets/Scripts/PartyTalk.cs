using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PartyTalk : MonoBehaviour
{
    public string text = "";

    [SerializeField] private TMP_Text TXT_notif;
    [SerializeField] private TMP_Text TXT_input;
    [SerializeField] private NPCinfo npc;
    [SerializeField] private EnterParty manager;

    [Header("Park Settings")]
    public bool isSquirrel = false;
    public bool isDeer = false;
    public bool isFish = false;
    public bool isPolice = false;
    public bool specialGem = false;

    private int talkStep = 0;
    private bool isInRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            // Squirrel: 3 max for talks, 4 for success
            // Deer: 3 max for talks, 4 for success
            // Police: 5 max for talks, 6 when accept, 7 when success

            if (!manager.completedSquirrel && isSquirrel && talkStep < 3) talkStep++;
            else if (manager.completedSquirrel && isSquirrel) talkStep = 4;

            else if (!manager.acceptPolice && !manager.helpedPolice && isPolice && talkStep < 5) talkStep++;
            else if (manager.acceptPolice && !manager.helpedPolice && isPolice) talkStep = 6;
            else if (manager.acceptPolice && manager.helpedPolice && isPolice) talkStep = 7;

            else if (!manager.joinedMafia && talkStep < 3 && isDeer) talkStep++;
            else if (manager.joinedMafia && isDeer) talkStep = 4;

        }
    }

    public void UpdateTalks()
    {

        if (isSquirrel)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("Hey, you again!");
                    TXT_input.SetText("[E] Talk to " + npc.charName);
                    break;
                case 1:
                    TXT_notif.SetText("Turns out the parkour wasnt a success..");
                    TXT_input.SetText("[E] Sorry to hear that buddy..");
                    break;
                case 2:
                    TXT_notif.SetText("But I decided to still make a little parkour!");
                    TXT_input.SetText("[E] Oh how fun!");
                    break;
                case 3:
                    TXT_notif.SetText("Try to reach me, ill wait at the end!");
                    TXT_input.SetText("");
                    break;
                case 4:
                    TXT_notif.SetText("Nice job!");
                    TXT_input.SetText("");
                    break;
            }
        }
        if (isDeer)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("Hey its you again!");
                    TXT_input.SetText("[E] Talk to " + npc.charName);
                    break;
                case 1:
                    TXT_notif.SetText("I'd invite you over, but I'm not sure the rest will trust you quite yet...");
                    TXT_input.SetText("[E] Oh.. Okay..");
                    break;
                case 2:
                    TXT_notif.SetText("Maybe you can show us something as a token of dedication and trust.");
                    TXT_input.SetText("[E] Okay I get what you mean.");
                    break;
                case 3:
                    TXT_notif.SetText("Come back later perhaps, when you can prove your worth.");
                    TXT_input.SetText("");
                    break;
                case 4:
                    TXT_notif.SetText("Whats up, homie. Nice gem!\n*wink*");
                    TXT_input.SetText("");
                    break;
            }
        }
        if (isPolice)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("Hey you! You helped us before.\nCome here!");
                    TXT_input.SetText("[E] Talk to the penguins");
                    break;
                case 1:
                    TXT_notif.SetText("So, we didn't tell you this last time.. but..");
                    TXT_input.SetText("[E] Yeah?");
                    break;
                case 2:
                    TXT_notif.SetText("We're the police, hunting down the mafia.");
                    TXT_input.SetText("[E] what! no way!");
                    break;
                case 3:
                    TXT_notif.SetText("And we believe to be on to them, but need someone to infiltrate.");
                    TXT_input.SetText("[E] Okay..?");
                    break;
                case 4:
                    TXT_notif.SetText("We want you to do it!");
                    TXT_input.SetText("[E] What?!");
                    break;
                case 5:
                    TXT_notif.SetText("Are you up for the challenge?");
                    TXT_input.SetText("[1] Agree | [2] Consider | [3] Refuse");
                    break;
                case 6:
                    TXT_notif.SetText("We need you to find a way in..");
                    TXT_input.SetText("");
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(text != null || text != "") TXT_notif.SetText(text);
            isInRange = true;
        }
        UpdateTalks();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TXT_notif.SetText("");
            isInRange = false;
        }
    }
}
