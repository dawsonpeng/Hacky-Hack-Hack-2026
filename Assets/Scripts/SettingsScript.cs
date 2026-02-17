using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsScript : MonoBehaviour
{
    public static float TickSpeed { get; private set; } = 1f;
    public Volume volume;
    private ColorAdjustments colorAdjust;
    // the game's main camera
    public CinemachineCamera virtualCam;
    private float baseFixedDeltaTime;

    [Header("Day/Night grounds")]
    public GameObject lightground;
    public GameObject darkground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGet(out colorAdjust);
        baseFixedDeltaTime = Time.fixedDeltaTime;
        UpdateGroundVisibility();
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
        UpdateGroundVisibility();
    }

    private void UpdateGroundVisibility()
    {
        bool isDay = colorAdjust != null && colorAdjust.postExposure.value >= 0f;
        if (lightground != null) lightground.SetActive(isDay);
        if (darkground != null) darkground.SetActive(!isDay);
    }

    // changes the brightness somehow??? GPT ahh
    public void ChangeBrightness(float value)
    {
        colorAdjust.postExposure.value = value;
    }

    // changes the camera's field of view
    public void SetCameraFOV(float value)
    {
        // Debug.Log("slider changed");
        virtualCam.Lens.OrthographicSize = value;
    }

    // sets how fast the game updates (1 = normal, 0.5 = half speed, 2 = double speed)
    public void SetTickSpeed(float tickSpeed)
    {
        TickSpeed = Mathf.Max(0.01f, tickSpeed);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = baseFixedDeltaTime;
    }
}
