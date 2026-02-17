using UnityEngine;

public class playerScript : MonoBehaviour
{
    private const string SelectedCharacterKey = "SelectedCharacter";

    public float flapStrength;
    public float moveSpeed = 5f;
    private Rigidbody2D body;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    [SerializeField] private string groundTag = "Ground";

    public bool IsGrounded;
    [SerializeField] private SliderScript sliderScript;
    [SerializeField] private ToggleScript toggleScript;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite chen_0Sprite;
    [SerializeField] private Sprite maleIdleSprite;

    public int extraJumpsValue = 1;
    private int extraJumps;
    private int groundContacts;

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
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        ApplySelectedCharacterSprite();

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(IsGrounded) {
                body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
            } else if (extraJumps > 0) {
                body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
                extraJumps--;
            }
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        powerupScript powerup = other.GetComponent<powerupScript>();
        if (powerup == null)
        {
            if (other.CompareTag(groundTag))
            {
                groundContacts++;
                IsGrounded = groundContacts > 0;
            }
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(groundTag))
        {
            groundContacts = Mathf.Max(0, groundContacts - 1);
            IsGrounded = groundContacts > 0;
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

    private void ApplySelectedCharacterSprite()
    {
        int selection = PlayerPrefs.GetInt(SelectedCharacterKey, 1);
        Sprite selectedSprite = selection == 2 ? maleIdleSprite : chen_0Sprite;

        if (selectedSprite == null)
        {
            Debug.LogWarning("playerScript: assign character sprites in the Inspector.");
            return;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = selectedSprite;
        }
    }

}
