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

    private void OnTriggerEnter2D(Collider2D other)
    {
        CoinPowerup coinPowerup = other.GetComponent<CoinPowerup>();
        if (coinPowerup == null)
        {
            return;
        }

        coinPowerup.OnCollected();

        if (coinPowerup.isSlider)
        {
            if (sliderScript != null)
            {
                sliderScript.OnCoinPowerup(coinPowerup);
            }
        }
        else
        {
            if (toggleScript != null)
            {
                toggleScript.OnCoinPowerup(coinPowerup);
            }
        }
    }
}
