using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System;

public class ActionText: CutsceneManager
{
    public bool isActive = false;

    protected int numLines;

    new public static ActionText instance;

    protected Action endAction;

    bool shouldEndTurn = true;

    protected override void Start()
    {
        base.Start();

        instance = this;
    }

    public override void Increment()
    {
        if (!isActive) return;

        Debug.Log("Incremented");

        if (!lv.currentStopToken.CanInterrupt)
        {
            stage++;

            if(stage == numLines)
            {
                lv.UserRequestedViewAdvancement();
                stage = 0;
                isActive = false;

                // if(shouldEndTurn)
                //     FightScene.instance.EndTurn();

                if(endAction != null)
                {
                    endAction();
                }
            }
            else
            {
                lv.UserRequestedViewAdvancement();
            }
        }
        else
        {
            lv.UserRequestedViewAdvancement();
        }
        da.EndDialogue();
    }


    public void StartText(string node, string character, int lines, Action a = null, bool endTurn = true)
    {
        numLines = lines;

        stage = 0;

        isActive = true;

        endAction = a;

        shouldEndTurn = endTurn;

        SetChar(character);

        dr.StartDialogue(node);
    }
}