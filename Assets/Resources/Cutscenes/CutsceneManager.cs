using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement;
using Yarn.Unity;
using TMPro;

public class CutsceneManager : LevelManager
{
    new public static CutsceneManager instance;

    [SerializeField] protected Animator[] objects;

    // [SerializeField] protected AudioClip[] sounds;

    protected static AudioClip[] voiceClips;

    // [SerializeField] protected TMP_FontAsset[] fonts;

    protected string currentChar;

    public int stage = 0;

    protected LineView lv;

    protected DialogueRunner dr;

    protected TMP_Text tmp;

    protected RectTransform rt;

    protected bool skipping;

    protected bool isWaitingForInput = false;

    protected DialogueAudio da;

    [SerializeField] 
    protected bool doNotSave;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        dialogueVol = 1.0f / audioSource.volume;

        audioSource.Play();

        if (voiceClips == null)
        {
            voiceClips = Resources.LoadAll<AudioClip>("Cutscenes/Voice Clips");
        }

        lv = GetComponent<LineView>();
        da = GetComponent<DialogueAudio>();
        if(GameObject.Find("Dialogue System") != null)
            dr = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
        // tmp = transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        rt = GetComponent<RectTransform>();

        nextScene = "Day 2 End";

        if(dr != null)
            dr.StartDialogue("LoadVars");

        if (!doNotSave)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // if(currentScene.Substring(0, 3) == "Day")
            //     currentScene = currentScene.Substring(0, 5) + "/" + currentScene;

            PlayerPrefs.SetString("currentScene", currentScene);
        }
    }

    protected override void Update()
    {
    }


    public virtual void Increment()
    {
        lv.UserRequestedViewAdvancement();
        EndLevel();

        // lv.UserRequestedViewAdvancement();
        // stage++;
    }

    // public virtual void Skip()
    // {
    //     if (isWaitingForInput) return;

    //     SkipScene();
    // }

    // public void SelectOption(GameObject selector)
    // {
    //     EventSystem.current.SetSelectedGameObject(null);
    //     selector.SetActive(false);
    //     audioSource.PlayOneShot(Resources.Load<AudioClip>("UI/select"));
    //     isWaitingForInput = false;
    //     Increment();
    // }

    public void SetChar(string newChar)
    {
        currentChar = newChar;

        AudioClip clip = null;

        foreach (AudioClip a in voiceClips)
        {
            if (a.name == currentChar)
            {
                clip = a;
                break;
            }
        }

        da.setClip(clip);

        // if (newChar == "Nayeli")
        // {
        //     rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 90);
        // }
        // else if (newChar == "Shae" || newChar == "Servantes" || newChar == "Generic 3")
        // {
        //     rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 70);
        // }
        // else if (rt.anchoredPosition.y != 100)
        // {
        //     rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 100);
        // }

        // if (fonts.Length > 1)
        // {
        //     TMP_FontAsset newFont;
        //     if (newChar == "Third Person")
        //     {
        //         newFont = fonts[1];
        //     }
        //     else
        //     {
        //         newFont = fonts[0];
        //     }

            //     tmp.font = newFont;
            // }
    }

    // public void SkipScene()
    // {
    //     audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/select"));

    //     skipping = true;
    //     EndLevel();
    // }

    // public override void EndLevel()
    // {
    //     base.EndLevel();
    //     skipping = true;
    // }

    [YarnFunction("uppercase")]
    public static string Uppercase(string text)
    {
        return text.ToUpper();
    }
}
