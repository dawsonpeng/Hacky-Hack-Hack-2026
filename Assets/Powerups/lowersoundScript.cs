using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class lowersoundScript : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float targetVolume = 0.1f;
    [SerializeField] private bool disableAfterTrigger = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D triggerCollider;

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

    void Reset()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerScript player = other.GetComponent<playerScript>();
        if (player == null)
        {
            return;
        }

        AudioListener.volume = Mathf.Clamp01(targetVolume);

        if (disableAfterTrigger)
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
}
