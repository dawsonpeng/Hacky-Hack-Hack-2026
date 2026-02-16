using UnityEngine;

public class finish: MonoBehaviour
{
    public GameObject winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winUI.SetActive(true);
        }
    }
}
