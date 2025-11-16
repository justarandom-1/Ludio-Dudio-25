using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float totalTime;
    private float timeLeft;

    private GameObject text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = totalTime;
        rb = GetComponent<Rigidbody2D>();
        text = GameObject.Find("TimeText");
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Reset();
        }
        //DEBUG
        text.GetComponent<TMP_Text>().text = "Time Left: " + timeLeft.ToString();
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 dir = inputValue.Get<Vector2>();
        rb.linearVelocity = dir * speed;
    }
    void Reset()
    {
        //do animation stuff maybe?

        transform.position = Vector2.zero;
        timeLeft = totalTime;
    }
}
