using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsScript : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjust;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGet(out colorAdjust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // changes the volume, values range from 0 to 1
    public void SetVolume(float vol)
    {
        // Debug.Log("Volume: " + vol);
        AudioListener.volume = vol;
    }

    public void ChangeVolume(float deltaVol) {
        AudioListener.volume += deltaVol;
    }

    [ContextMenu("debright")]
    public void ToggleDay() {
        Debug.Log("toggled day");
        if (colorAdjust.postExposure.value < 0) {
            ChangeBrightness(0f);
        } else {
            ChangeBrightness(-4f);
        }
    }

    // changes the brightness somehow??? GPT ahh
    public void ChangeBrightness(float value)
    {
        colorAdjust.postExposure.value = value;
    }
}
