using UnityEngine;

public class powerupScript : MonoBehaviour
{
    public bool isSlider = true;
    public int lowestValue = 0;
    public int highestValue = 100;
    public bool isActive = true;

    [SerializeField] protected SpriteRenderer powerupRenderer;
    [SerializeField] protected Collider2D triggerCollider;

    protected virtual void Awake()
    {
        if (powerupRenderer == null)
        {
            powerupRenderer = GetComponent<SpriteRenderer>();
        }

        if (triggerCollider == null)
        {
            triggerCollider = GetComponent<Collider2D>();
        }
    }

    public virtual void OnCollected()
    {
        if (powerupRenderer != null)
        {
            powerupRenderer.enabled = false;
        }

        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }
}
