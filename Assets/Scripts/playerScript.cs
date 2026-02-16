using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float flapStrength;
    public float moveSpeed = 5f;
    private Rigidbody2D body;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public bool IsGrounded;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(movement * moveSpeed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
        }
    }
    
    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Death")
        {
            Die();
        }
    }

    private void Die()
    {
        spriteRenderer.color = Color.red;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
