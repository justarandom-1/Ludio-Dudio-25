using TMPro;
// using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Vector2 xRange;

    private GameObject interacting;

    [SerializeField] Vector2 yRange;

    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private Animator animator;

    private Vector2 movementVector = Vector2.zero;

    private AudioSource audioSource;

    [SerializeField] float dashLimit;

    private bool isDashHeld = false;
    private bool isDashing = false;
    private float dashTime = 0;
    private float direction;
    private bool isAtMouse = false;

    [SerializeField] AudioClip dashSFX;

    public static Player instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        GameObject child = transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody2D>();
        animator = child.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        direction = Mathf.Sign(transform.localScale.x);

        // input = GetComponent<PlayerInput>();
    }

    void OnMouseOver()
	{
        isAtMouse = true;
        rb.linearVelocity = Vector2.zero;
	}

    void OnMouseExit()
	{
        isAtMouse = false;
	}

    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && dashTime < dashLimit)
        {
            isDashHeld = true;
        }

        if((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) && isDashHeld)
        {
            isDashHeld = false;
        }
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

    void FixedUpdate()
    {
        if(!isAtMouse){
            movementVector = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            direction = Mathf.Sign(movementVector.x);

            float angle = direction * Vector2.SignedAngle(Vector2.right * direction, movementVector);

            animator.SetFloat("angle", angle);

            if(direction != Mathf.Sign(transform.localScale.x)){
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            rb.linearVelocity = movementVector.normalized * speed;
            // PlayerAnimator.SetInteger("Verticaldirection", (int)movementVector.y);

            if(isDashHeld && !isDashing && (movementVector.x != 0 || movementVector.y != 0)){
                isDashing = true;
                audioSource.PlayOneShot(dashSFX);
            }

            if(!isDashHeld && isDashing){
                isDashing = false;
                dashTime = 0;
            }

            if(isDashing){
                dashTime += Time.deltaTime;
                if(dashTime >= dashLimit){
                    isDashHeld = false;
                    isDashing = false;
                    dashTime = 0;
                }else{
                    rb.linearVelocity *= 2;
                }
            }
        }
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

        // transform.position = Vector3.forward * transform.position.z;
        // timeLeft = totalTime;
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }

    public Vector2 isInBoundary()
    {
        float a = 1;
        
        float b = 1;
        if(transform.position.x < xRange[0] || transform.position.x > xRange[1])
            a = 0;

        if(transform.position.y < yRange[0] || transform.position.y > yRange[1])
            b = 0;
        return new Vector2(a, b);
    }
}
