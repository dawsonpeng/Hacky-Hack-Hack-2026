using UnityEngine;

public class playerScript : MonoBehaviour
{
    private const string SelectedCharacterKey = "SelectedCharacter";

    public float flapStrength;
    public float moveSpeed = 5f;
    private Rigidbody2D body;
    [SerializeField] private bool applyNoFrictionMaterial = true;
    private PhysicsMaterial2D noFrictionMaterial;

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
    private Vector3 checkpointPosition;
    private bool hasCheckpoint;
    private Color baseColor = Color.white;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (applyNoFrictionMaterial)
        {
            ApplyNoFrictionMaterial();
        }
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
        if (spriteRenderer != null)
        {
            baseColor = spriteRenderer.color;
        }
        ApplySelectedCharacterSprite();

        extraJumps = extraJumpsValue;
        checkpointPosition = transform.position;
        hasCheckpoint = true;
    }

    private void ApplyNoFrictionMaterial()
    {
        if (noFrictionMaterial == null)
        {
            noFrictionMaterial = new PhysicsMaterial2D("PlayerNoFriction")
            {
                friction = 0f,
                bounciness = 0f
            };
        }

        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].sharedMaterial = noFrictionMaterial;
        }
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
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
        SettingsScript settings = FindObjectOfType<SettingsScript>();
        if (settings != null)
        {
            settings.SetTickSpeed(1f);
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (hasCheckpoint)
        {
            RespawnAtCheckpoint();
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;
    }

    private void RespawnAtCheckpoint()
    {
        transform.position = checkpointPosition;
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0f;
        groundContacts = 0;
        IsGrounded = false;
        extraJumps = extraJumpsValue;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = baseColor;
        }
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
