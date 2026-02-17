using UnityEngine;
using System.Collections;

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

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip deathSound;

    private bool LandSoundDebounce = false;
    public bool IsAlive = true;

    public float squashAmount = 0.25f;
    public float stretchAmount = 0.35f;
    public float squashSpeed = 8f;

    private Vector2 originalScale;
    private Vector2 targetScale;


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
            sliderScript = FindFirstObjectByType<SliderScript>();
        }

        if (toggleScript == null)
        {
            toggleScript = FindFirstObjectByType<ToggleScript>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        ApplySelectedCharacterSprite();
        extraJumps = extraJumpsValue;
        audioSource = GetComponent<AudioSource>();
        originalScale = spriteRenderer.size;
        targetScale = originalScale;
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

    private void ResetDebounce()
    {
        LandSoundDebounce = false;
    }

    private void UpdateSlimeVisual()
    {
        float verticalVelocity = body.linearVelocity.y;

        // Stretch when moving upward (jumping)
        if (verticalVelocity > 0.1f)
        {
            targetScale = new Vector2(
                originalScale.x - stretchAmount,
                originalScale.y + stretchAmount
            );
        }
        // Squash when landing / falling fast
        else if (verticalVelocity < -3f)
        {
            targetScale = new Vector2(
                originalScale.x - squashAmount,
                originalScale.y + squashAmount
            );
        }
        else
        {
            targetScale = originalScale;
        }   

        // Interpolate
        spriteRenderer.size = Vector2.Lerp(
            spriteRenderer.size,
            targetScale,
            Time.deltaTime * squashSpeed
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            float movement = Input.GetAxis("Horizontal");
            body.linearVelocity = new Vector2(movement * moveSpeed, body.linearVelocity.y);

            if (IsGrounded)
            {
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (IsGrounded)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
                    PlayAudio(jumpSound, 0.5f);
                }
                else if (extraJumps > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, flapStrength);
                    extraJumps--;
                    PlayAudio(jumpSound, 0.5f);
                }
            }
        }
        UpdateSlimeVisual();
    }

    private void PlayAudio(AudioClip audioToPlay, float volume)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(audioToPlay, volume);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!LandSoundDebounce)
        {
            LandSoundDebounce = true;
            PlayAudio(landSound, 0.1f);
            Invoke(nameof(ResetDebounce), 1f);
        }
        
        powerupScript powerup = other.GetComponent<powerupScript>();
        if (powerup == null)
        {
            if (other.CompareTag(groundTag))
            {
                groundContacts++;
                IsGrounded = groundContacts > 0;

                spriteRenderer.size = new Vector2(
                    originalScale.x + squashAmount * 1.5f,
                    originalScale.y - squashAmount * 1.5f
                );
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Die();
        }
    }
    
    private void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        if (IsAlive)
        {
            IsAlive = false;
            spriteRenderer.color = Color.red;
            PlayAudio(deathSound, 0.5f);
        }
        SettingsScript settings = FindFirstObjectByType<SettingsScript>();
        if (settings != null)
        {
            settings.SetTickSpeed(1f);
        }
        else
        {
            Time.timeScale = 1f;
        }
        Invoke(nameof(ChangeScene), 2.0f);   
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
