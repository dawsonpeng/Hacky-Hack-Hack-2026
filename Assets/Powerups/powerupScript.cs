using UnityEngine;

public class powerupScript : MonoBehaviour
{
    public bool isSlider = true;
    public int lowestValue = 0;
    public int highestValue = 100;
    public bool isActive = true;
    // this variable stores any popUp that should appear whne we enter this power-up
    public GameObject popUpWindow;

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
        if (popUpWindow != null) {
            CreatePopUp();
        }
    }

    public void CreatePopUp()
    {
        Debug.Log("Pop Up Created");
        popUpWindow.SetActive(true);
    }
}
