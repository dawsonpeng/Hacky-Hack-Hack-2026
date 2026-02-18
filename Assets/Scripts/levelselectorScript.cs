using UnityEngine;
using UnityEngine.SceneManagement;
public class levelselectorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void level3()
    {
        SceneManager.LoadScene("level 3");
    }

    public void level4()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void level5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
