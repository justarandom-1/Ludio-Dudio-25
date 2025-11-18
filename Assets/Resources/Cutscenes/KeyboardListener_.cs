using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using Yarn.Unity;

public class KeyboardListener_ : MonoBehaviour
{
    //mostly to check if we already exist
    public static KeyboardListener_ instance;

    // private bool keyHeld = true;

    private CutsceneManager cm;
    // private ActionText at;

    private bool isAtStart = true;
    private bool isActive = true;

    private void Start()
    {
        if(instance != null)
        {
            isActive = false;
            return;
        }   

        instance = this;
        // DontDestroyOnLoad(gameObject);

        cm = GetComponent<CutsceneManager>();

        // at = GetComponent<ActionText>();    
    }

    // public void Skip()
    // {
    //     EventSystem.current.SetSelectedGameObject(null);

    //     cm.Skip();
    // }

    public void Update()
    {
        // if(!Input.anyKey){
        //     keyHeld = false;
        // }

        // if(Input.anyKey && !keyHeld){
        //     keyHeld = true;

        //     cm.Increment();
        // }   

        if(!isActive)
        {
            return;
        }

        if(isAtStart)
        {
            isAtStart = false;
            return;
        }

        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (Input.anyKeyDown && cm != null && 
            (selected == null || selected.name != "SkipButton"))
        {
            cm.Increment();
        }
    }
}
