using UnityEngine;

public class pausemenu : MonoBehaviour
{
    public GameObject container;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(!container.activeSelf);
            Time.timeScale = container.activeSelf ? 0 : 1;
        }
    }

    public void resumeGame()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
