using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private Color activatedColor = new Color(0.4f, 1f, 0.4f, 1f);
    [SerializeField] private bool disableAfterActivate = false;
    [SerializeField] private bool hideAfterActivate = false;

    private bool isActivated = false;

    private void Awake()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated)
        {
            return;
        }

        playerScript player = other.GetComponent<playerScript>();
        if (player == null || !player.IsAlive)
        {
            return;
        }

        Vector3 checkpointPosition = triggerCollider != null
            ? triggerCollider.bounds.center
            : transform.position;
        player.SetCheckpoint(checkpointPosition);
        Activate();
    }

    private void Activate()
    {
        isActivated = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = activatedColor;
        }

        if (hideAfterActivate && spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (disableAfterActivate && triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }
}
