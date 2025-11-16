using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float totalTime;
    [SerializeField] float offset;

    [SerializeField] Image displayMask;
    [SerializeField] RectTransform displayInterval;


    private bool hasStarted = false;
    private bool hasEnded = false;

    private float maxWidth;
    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasEnded)
        {
            return;
        }

        timer += Time.deltaTime;

        if(!hasStarted)
        {
            if(timer > offset)
            {
                hasStarted = true;
                timer -= offset;
            }
            return;
        }

        if(timer > totalTime)
        {
            LevelManager.instance.Lose();
            displayMask.fillAmount = 1f;
            hasEnded = true;
            return;
        }

        float progress = timer / totalTime;
        
        displayMask.fillAmount = progress;

        displayInterval.anchoredPosition = Vector2.right * Mathf.Lerp(230f, -230f, progress);
    }
}
