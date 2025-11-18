using UnityEngine;

public class Dolphin : MonoBehaviour
{

    private bool hasInteracted = false;
    // private int type;

    private Vector2 movementVector;

    // [SerializeField] int[] lines;

    [SerializeField] float speed;

    [SerializeField] float startDistance;

    private SpriteRenderer spriteRenderer;

    // [SerializeField] float bottom;

    // private Collider2D collider;

    // private Animator animator;

    private bool isInRange;

    private bool colliderEnabled = true;

    private float hitTimer = 0;

    private float offsetTimer = 0;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 playerPos = Player.instance.getPosition();

        do
        {
            float angle = Random.Range(0, 360f);
            transform.position = playerPos + Vector2.right * startDistance * Mathf.Cos(angle) + Vector2.up * startDistance * Mathf.Sin(angle);
        }while(transform.position.x > 200);

        // type = Random.Range(0, lines.Length) + 1;

        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        movementVector = (playerPos - (Vector2)transform.position).normalized;

        transform.parent.localScale = new Vector3(-Mathf.Sign(movementVector.x), 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 playerPos = Player.instance.getPosition();

        if(hasInteracted && colliderEnabled)
        {
            hitTimer += Time.deltaTime;
            if(hitTimer > 1)
            {
                GetComponent<Collider2D>().enabled = false;
                colliderEnabled = false;
            }

        }

        if(transform.position.magnitude > 400)
        {
            Destroy(gameObject);
        }

        if(isInRange)
        {
            offsetTimer += Time.deltaTime;
            if(offsetTimer > 0.5f && !spriteRenderer.isVisible)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(spriteRenderer.isVisible)
            {
                isInRange = true;
                GetComponent<AudioSource>().Play();
            }
            movementVector = (playerPos - (Vector2)transform.position).normalized;
        }

        rb.linearVelocity = movementVector * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasInteracted && other.gameObject.CompareTag("Player"))
        {
            Player.instance.stun();
            hasInteracted = true;
        }
    }
}
