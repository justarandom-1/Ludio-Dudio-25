using System.Collections.Generic;
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

    protected GameObject holder;

    protected Animator animator;

    protected List<Animator> characters;

    protected string node;

    protected override void Start()
    {
        base.Start();

        instance = this;

        holder = transform.parent.gameObject;
        animator = holder.GetComponent<Animator>();
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

                if(shouldEndTurn)
                    EndDialogue();

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
            // lv.UserRequestedViewAdvancement();
        }
        da.EndDialogue();
    }



    public void Initialize(int[] characterList, string speakingCharacter, string dialogueNode, int lines)
    {
        Freeze();
        
        animator.Play("StartInteraction");

        characters = new List<Animator>();

        for(int i = 0; i < characterList.Length; i++)
        {
            characters.Add(objects[characterList[i]]);
        }

        node = dialogueNode;

        numLines = lines;

        stage = 0;

        isActive = true;
        
        SetChar(speakingCharacter);
    }


    public void startCharacters()
    {
        foreach(Animator character in characters)
        {
            character.Play("fadeIn");
        }
    }

    public void startDialogue()
    {
        Debug.Log("started dialogue");
        dr.StartDialogue(node);
    }

    public void EndDialogue()
    {
        foreach(Animator character in characters)
        {
            character.Play("fadeOut");
        }
        animator.Play("EndInteraction");
    }

    public void Freeze()
    {
        Time.timeScale = 0;
    }

    public void unFreeze()
    {
        Time.timeScale = 1;
    }


    // public void StartText(string node, string character, int lines, Action a = null, bool endTurn = true)
    // {
    //     numLines = lines;

    //     stage = 0;

    //     isActive = true;

    //     endAction = a;

    //     shouldEndTurn = endTurn;

    //     SetChar(character);

    //     dr.StartDialogue(node);
    // }
}