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
    private Vector3 cameraOffset;

    private GameObject interacting;

    private GameObject text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = totalTime;
        rb = GetComponent<Rigidbody2D>();
        text = GameObject.Find("TimeText");
        cameraOffset = Camera.main.transform.position - transform.position;
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
    void LateUpdate()
    {
        //camera follow
        // Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        // if (pos.x > 0.7)
        // {
        //     Camera.main.transform.Translate(Vector3.right * speed * Time.deltaTime);
        // }
        // else if (pos.x < 0.3)
        // {
        //     Camera.main.transform.Translate(Vector3.left * speed * Time.deltaTime);
        // }

        // if (pos.y > 0.7)
        // {
        //     Camera.main.transform.Translate(Vector3.up * speed * Time.deltaTime);
        // }
        // else if (pos.y < 0.3)
        // {
        //     Camera.main.transform.Translate(Vector3.down * speed * Time.deltaTime);
        // }
        Camera.main.transform.position = transform.position + cameraOffset;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        interacting = collision.gameObject;
        //perhaps show an E above the character
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        interacting = null;
        //remove the E if you show it
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 dir = inputValue.Get<Vector2>();
        rb.linearVelocity = dir * speed;
    }
    void OnInteract(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (interacting != null)
            {
                //perform interact action
                Debug.Log("interacted with " + interacting.name);
            }
        }
    }
    void Reset()
    {
        //do animation stuff maybe?

        transform.position = Vector2.zero;
        timeLeft = totalTime;
    }
}
