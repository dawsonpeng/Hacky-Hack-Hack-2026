using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionScript : MonoBehaviour
{
    private const string SelectedCharacterKey = "SelectedCharacter";

    [SerializeField] private Image selectionPreviewImage;
    [SerializeField] private Sprite chen_0Sprite;
    [SerializeField] private Sprite maleIdleSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplySavedSelectionPreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectCharacter1(){
        PlayerPrefs.SetInt(SelectedCharacterKey, 1);
        PlayerPrefs.Save();
        SetCharacterSprite(chen_0Sprite);
    }

    public void SelectCharacter2(){
        PlayerPrefs.SetInt(SelectedCharacterKey, 2);
        PlayerPrefs.Save();
        SetCharacterSprite(maleIdleSprite);
    }

    private void SetCharacterSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("CharacterSelectionScript: sprite is not assigned.");
            return;
        }

        if (selectionPreviewImage != null)
        {
            selectionPreviewImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("CharacterSelectionScript: assign selectionPreviewImage (white box).");
        }
    }

    private void ApplySavedSelectionPreview()
    {
        int selection = PlayerPrefs.GetInt(SelectedCharacterKey, 1);
        Sprite selectedSprite = selection == 2 ? maleIdleSprite : chen_0Sprite;
        SetCharacterSprite(selectedSprite);
    }
}
