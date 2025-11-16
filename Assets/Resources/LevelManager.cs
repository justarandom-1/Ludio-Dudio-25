using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] protected string winScene;

    [SerializeField] protected string loseScene;
    protected int state;
    protected AudioSource audioSource;
    protected string nextScene;
    protected int maxLevel;

    public float dialogueVol;


    private static Texture2D cursorTexture;
    private static Vector2 hotSpot;

    protected virtual void Awake()
    {
        if(cursorTexture == null)
        {
            cursorTexture = Resources.Load<Texture2D>("UI/cursor.png");
            hotSpot = new Vector2(32, 60);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        dialogueVol = 1.0f / audioSource.volume;

        audioSource.Play();
    }

    // public static void CursorOn()
    // {
    //     Cursor.visible = true;
    //     Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    // }

    // public static void CursorOff()
    // {
    //     Cursor.visible = false;
    // }

    public virtual void Win()
    {
        // int levelNum = SceneManager.GetActiveScene().name[5] - '0';

        nextScene = winScene;
        // PlayerPrefs.SetInt("maxLevel", Mathf.Max(maxLevel, levelNum + 1));
        EndLevel();
    }

    public void Lose()
    {
        nextScene = loseScene;
            // PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().name[5] - '0');
        EndLevel();
    }

    public virtual void EndLevel()
    {
        state = 2;

        GameObject.Find("Black").GetComponent<Animator>().Play("fadeIn");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        state2();

        // if (state != 2)
        //     Win();
    }

    protected virtual void state2()
    {
        if (state == 2)
        {
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime / dialogueVol, 0);

            if (audioSource.volume == 0)
            {
                DataStorage.pushData();
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void PlayAudio(AudioClip a, float vol = 1.0f)
    {
        audioSource.PlayOneShot(a, vol * dialogueVol);
    }

    public static float Vec2Deg(Vector2 v)
    {
        v.Normalize();
        float a = Mathf.Asin(v.y) * Mathf.Rad2Deg;
        if (v.x < 0)
            a = 180 - a;
        return a;
    }
    
    public static Vector2 Deg2Vec(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

}
