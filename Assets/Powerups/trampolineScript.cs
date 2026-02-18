using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class trampolineScript : MonoBehaviour
{
    [SerializeField] private bool requirePlayerTag = true;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float minBounceVelocity = 6f;
    [SerializeField] private float maxBounceVelocity = 18f;

    private Collider2D trampolineCollider;

    void Awake()
    {
        trampolineCollider = GetComponent<Collider2D>();
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
        ApplyBounce(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyBounce(collision.collider);
    }

    private void ApplyBounce(Collider2D other)
    {
        if (other == null)
        {
            return;
        }

        if (requirePlayerTag && !other.CompareTag(playerTag))
        {
            return;
        }

        Rigidbody2D body = other.attachedRigidbody;
        if (body == null)
        {
            return;
        }

        float volume = Mathf.Clamp01(AudioListener.volume);
        float bounceVelocity = Mathf.Lerp(minBounceVelocity, maxBounceVelocity, volume);
        body.linearVelocity = new Vector2(body.linearVelocity.x, bounceVelocity);
    }
}
