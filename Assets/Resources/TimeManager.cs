using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float totalTime;
    [SerializeField] float offset;

    [SerializeField] Image displayMask;
    [SerializeField] RectTransform displayInterval;


    [SerializeField] Vector2 fillRange;
    [SerializeField] Vector2 intervalRange;


    private bool hasStarted = false;
    private bool hasEnded = false;

    private float maxWidth;
    private float timer = 0;

    void Awake()
    {
        displayMask.fillAmount = fillRange.x;

        displayInterval.anchoredPosition = Vector2.right * intervalRange.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasEnded)
        {
            return;
        }

        timer += Time.unscaledDeltaTime;

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
        
        displayMask.fillAmount = Mathf.Lerp(fillRange.x, fillRange.y, progress);

        displayInterval.anchoredPosition = Vector2.right * Mathf.Lerp(intervalRange.x, intervalRange.y, progress);
    }
}
