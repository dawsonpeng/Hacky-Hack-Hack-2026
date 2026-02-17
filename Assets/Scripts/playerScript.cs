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
    [SerializeField] private SliderScript sliderScript;
    [SerializeField] private ToggleScript toggleScript;

    private SpriteRenderer spriteRenderer;

    public int extraJumpsValue = 1;
    private int extraJumps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (sliderScript == null)
        {
            sliderScript = FindObjectOfType<SliderScript>();
        }

        if (toggleScript == null)
        {
            toggleScript = FindObjectOfType<ToggleScript>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(movement * moveSpeed, body.linearVelocity.y);

        if (IsGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded) {
                body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
            } else if (extraJumps > 0) {
                body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
                extraJumps--;
            }
            
        }
    }
    
    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        powerupScript powerup = other.GetComponent<powerupScript>();
        if (powerup == null)
        {
            return;
        }

        Debug.Log(powerup.isSlider
            ? $"Powerup hit: slider {powerup.lowestValue}-{powerup.highestValue}"
            : $"Powerup hit: toggle isActive={powerup.isActive}");

        powerup.OnCollected();

        if (powerup.isSlider)
        {
            if (sliderScript != null)
            {
                sliderScript.OnCoinPowerup(powerup);
            }
        }
        else
        {
            if (toggleScript != null)
            {
                toggleScript.OnCoinPowerup(powerup);
            }
        }
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
