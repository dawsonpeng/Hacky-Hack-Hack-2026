using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
