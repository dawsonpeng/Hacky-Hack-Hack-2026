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

    public void level1(){
        SceneManager.LoadScene("MainScene");
    }

    public void level2(){
        //todo
    }

    public void level3(){
        //todo
    }

    public void level4(){
        //todo
    }
    public void level5(){
        //todo
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
