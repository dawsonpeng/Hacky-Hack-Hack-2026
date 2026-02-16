using UnityEngine;

public class restart : MonoBehaviour
{
    public void LoadCurrentScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
