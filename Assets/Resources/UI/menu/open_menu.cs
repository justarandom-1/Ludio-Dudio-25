using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class open_menu : LevelManager
{
    // Start is called before the first frame update

    [SerializeField] bool isTitleScreen;

    [SerializeField] GameObject nameInput;

    protected override void Start()
    {
        base.Start();

        audioSource.Play();

        // LevelManager.CursorOn();

        if (isTitleScreen)
        {
            if (PlayerPrefs.GetInt("hasSeenQI", 0) == 0)
            {
                GameObject.Find("QI").SetActive(false);
            }

            if (PlayerPrefs.GetInt("numPreviousResets", -1) == -1)
            {
                GameObject.Find("Menu").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Menu").transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        // int noHits = PlayerPrefs.GetInt("noHits", 0);

        // int mostHits = PlayerPrefs.GetInt("mostHits", 0);

        // maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        // Debug.Log("Max Level: " + maxLevel);

        // if(3 <= maxLevel && maxLevel < 6){
        //     GameObject.Find("darkBeach").GetComponent<Image>().enabled = true;
        // }else if(6 <= maxLevel && maxLevel < 8){
        //     GameObject.Find("NU").GetComponent<Image>().enabled = true;
        // }else if(maxLevel == 8){
        //     GameObject.Find("End").GetComponent<Image>().enabled = true;
        //     audioSource.Stop();
        //     audioSource.clip = Resources.Load<AudioClip>("Music/Answer");
        //     audioSource.Play();
        // }

        // if (maxLevel == 0)
        // {
        //     GameObject.Find("LevelMenu").SetActive(false);
        //     // GameObject.Find("Dog").transform.GetChild(0).gameObject.SetActive(true);
        //     GameObject.Find("Protag").transform.GetChild(0).gameObject.SetActive(true);
        //     return;
        // }

        // GameObject.Find("StartMenu").SetActive(false);

        // if (maxLevel == 6)
        //     GameObject.Find("Dog").transform.GetChild(4).gameObject.SetActive(false);
        // else
        //     GameObject.Find("Dog").transform.GetChild(maxLevel - 1).gameObject.SetActive(true);


        // GameObject.Find("Protag").transform.GetChild(maxLevel - 1).gameObject.SetActive(true);

        // for (int i = 1; i <= 5; i++)
        // {
        //     GameObject levelButton = GameObject.Find("Level" + i.ToString());
        //     if (i >= maxLevel + 1)
        //         levelButton.GetComponent<Button>().interactable = false;
        //     levelButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        // if ((noHits & (1 << (i - 1))) != 0)
        //     levelButton.GetComponent<Image>().color = Color.red;

        // else if ((mostHits & (1 << (i - 1))) != 0)
        //     levelButton.GetComponent<Image>().color = Color.yellow;
        // }


    }
    // public void slideMenu(int i = 1){

    //     if(!currentDisplay.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("onDisplay")){
    //         return;
    //     }

    //     currentDisplay.GetComponent<Animator>().Play("slideOut" + i);
    //     displayedLevel += i;
    //     currentDisplay = GameObject.Find("levelPreview (" + displayedLevel + ")");
    //     currentDisplay.GetComponent<Animator>().Play("slideIn" + i);

    //     leftButton.interactable = displayedLevel > 0;
    //     rightButton.interactable = displayedLevel < maxLevel + 1 && displayedLevel < 8;

    //     audioSource.PlayOneShot(transitionSFX, 0.7F);
    // }

    public void Quit()
    {
        // This line works for built applications (e.g., standalone, mobile)
        Application.Quit();

        // This line is for quitting Play Mode within the Unity editor
        // It should be wrapped in a preprocessor directive to avoid including it in builds
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // #endif
    }

    public void SetName()
    {
        string name = nameInput.GetComponent<TMP_InputField>().text.Trim();

        DataStorage.setString("playerName", name);
    }

    public void StartLevel(string level)
    {

        audioSource.PlayOneShot(Resources.Load<AudioClip>("UI/select"));

        nextScene = level;
        EndLevel();

        // if(wait == 0){
        //     wait = 1.3F;
        //     audioSource.PlayOneShot(Resources.Load<AudioClip>("select"));
        //     GetComponent<Animator>().Play("fadeToBlack");
        // }
    }


    // Update is called once per frame
    protected override void Update()
    {
        state2();
    }
}
