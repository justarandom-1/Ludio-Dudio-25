using UnityEngine;

public class Slug : MonoBehaviour
{

    private bool hasInteracted = false;

    // private Vector2 movementVector;

    // [SerializeField] int[] lines;

    // [SerializeField] float speed;

    // [SerializeField] float bottom;

    // private Collider2D collider;

    // private Animator animator;

    // private bool isInRange;

    // private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // type = Random.Range(0, lines.Length) + 1;

        // rb = GetComponent<Rigidbody2D>();

        // movementVector = Vector2.down;

        // collider = GetComponent<Collider2D>();
        // animator = GetComponent<Animator>();

        // collider.enabled = false;
        // animator.enabled = false;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     Vector2 playerPos = Player.instance.getPosition();
    //     if(!isInRange)
    //     {
    //         if(((Vector2)transform.position - playerPos).magnitude < 10)
    //         {
    //             isInRange = true;
    //             collider.enabled = true;
    //             animator.enabled = true;
    //         }
    //     }

    //     if(transform.position.y < bottom || playerPos.y - transform.position.y > 30)
    //     {
    //         Destroy(gameObject);
    //     }

    //     rb.linearVelocity = movementVector * speed;
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasInteracted && other.gameObject.CompareTag("Player"))
        {
            hasInteracted = true;

            ActionText.instance.Initialize(new int[]{0, 2}, "Slug", "Slug", 4);
        }
    }
}
