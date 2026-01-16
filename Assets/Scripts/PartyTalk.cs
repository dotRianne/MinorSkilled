using System.Data;
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
    public bool isChicken = false;
    public bool isDeer = false;
    public bool isFish = false;
    public bool isPolice = false;
    public bool specialGem = false;

    public int talkStep = 0;
    public bool isInRange = false;

    private bool mafTruePolFalse = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            // Squirrel: 3 max for talks, 4 for success
            // Deer: 3 max for talks, 4 for success
            // Police: 5 max for talks, 6 when accept, 7 when success

            if (!manager.completedSquirrel && isSquirrel && talkStep < 4) talkStep++;
            else if (manager.completedSquirrel && isSquirrel) talkStep = 5;

            else if (!manager.acceptPolice && !manager.helpedPolice && isPolice && talkStep < 5) talkStep++;
            else if (!manager.acceptPolice && !manager.helpedPolice && isPolice && talkStep == 5)
            {
                manager.acceptPolice = true;
                talkStep = 6;
            }

            else if (!manager.joinedMafia && talkStep < 3 && isDeer) talkStep++;
            else if (!manager.joinedMafia && isDeer && talkStep == 3 && specialGem && !manager.acceptPolice) talkStep = 4;
            else if (!manager.joinedMafia && isDeer && talkStep == 3 && specialGem && manager.acceptPolice) talkStep = 5;
            else if (manager.joinedMafia && isDeer && talkStep == 4) talkStep = 6;
            else if (!manager.joinedMafia && isDeer && talkStep == 5) talkStep = 6;

            if (!manager.completedChicken && isChicken && talkStep < 6) talkStep++;
            else if (manager.completedChicken && isChicken) talkStep = 7;

            UpdateTalks();
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
                    TXT_notif.SetText("");
                    TXT_input.SetText("");
                    break;
                case 5:
                    TXT_notif.SetText("Nice job!");
                    TXT_input.SetText("");
                    break;
            }
        }
        if (isChicken)
        {
            switch (talkStep)
            {
                case 0:
                    TXT_notif.SetText("Hey man! Nice seeing you again!");
                    TXT_input.SetText("[E] Talk to " + npc.charName);
                    break;
                case 1:
                    TXT_notif.SetText("What's the password?");
                    TXT_input.SetText("[E] (internally: ???) ... What?");
                    break;
                case 2:
                    TXT_notif.SetText("What. Is. The. Password.");
                    TXT_input.SetText("[E] Chicken Butt?");
                    break;
                case 3:
                    TXT_notif.SetText("Good enough. Welcome, sibling.");
                    TXT_input.SetText("[E] What's going on here?");
                    break;
                case 4:
                    TXT_notif.SetText("We are summoning Cthulu. We need your help.");
                    TXT_input.SetText("[E] Okay, what do I need to do?");
                    break;
                case 5:
                    TXT_notif.SetText("We need you to light all the shrine's candles.");
                    TXT_input.SetText("[E] Okay, will do.");
                    break;
                case 6:
                    TXT_notif.SetText("Please light all the candles.");
                    TXT_input.SetText("");
                    break;
                case 7:
                    TXT_notif.SetText("Soon, Cthulu will set us free!");
                    TXT_input.SetText("His arrival will be welcomed.");
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
                    TXT_input.SetText("[E] *wink back*");
                    mafTruePolFalse = true;
                    manager.joinedMafia = true;
                    break;
                case 5:
                    TXT_notif.SetText("So the plan is as follows...");
                    TXT_input.SetText("[E] *Takes notes*");
                    mafTruePolFalse = false;
                    manager.helpedPolice = true;
                    break;
                case 6:
                    if (manager.joinedMafia) manager.ending = "mafia";
                    else if (manager.helpedPolice) manager.ending = "police";
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
                    TXT_input.SetText("[E] Agree");
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
            TXT_input.SetText("");
            isInRange = false;
        }
    }
}
