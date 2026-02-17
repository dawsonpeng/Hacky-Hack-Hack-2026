using UnityEngine;

public class resettickspeedScript : MonoBehaviour
{
    [SerializeField] private float resetTickSpeed = 1f;
    [SerializeField] private bool disableAfterReset = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D triggerCollider;

    private SettingsScript settings;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (triggerCollider == null)
        {
            triggerCollider = GetComponent<Collider2D>();
        }
    }

    void Start()
    {
        settings = FindObjectOfType<SettingsScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<playerScript>() == null)
        {
            return;
        }

        ResetTickSpeed();

        if (disableAfterReset)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
            }
        }
    }

    private void ResetTickSpeed()
    {
        if (settings == null)
        {
            settings = FindObjectOfType<SettingsScript>();
        }

        if (settings != null)
        {
            settings.SetTickSpeed(resetTickSpeed);
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
