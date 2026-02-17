using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string characterSelectionScene = "CharacterSelection";

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CharacterSelection()
    {
        Time.timeScale = 1;

        if (!Application.CanStreamedLevelBeLoaded(characterSelectionScene))
        {
            Debug.LogError($"Character selection scene '{characterSelectionScene}' is not in Build Settings.");
            return;
        }

        SceneManager.LoadScene(characterSelectionScene);
    }
}
