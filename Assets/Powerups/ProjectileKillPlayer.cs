using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileKillPlayer : MonoBehaviour
{
    private void HandleHit(GameObject other)
    {
        var player = other.GetComponent<playerScript>();
        if (player != null)
        {
            // Uses SendMessage to avoid changing playerScript visibility.
            player.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
            return;
        }

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other.gameObject);
    }
}


