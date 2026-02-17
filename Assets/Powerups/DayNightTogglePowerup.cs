using UnityEngine;

public class DayNightTogglePowerup : powerupScript
{
    public override void OnCollected()
    {
        base.OnCollected();
        // Popup is shown by base if popUpWindow is assigned. Toggle only when button is pressed.
    }

    /// <summary>Call this from the popup Button's On Click () in the Inspector. Toggles day/night and closes the popup.</summary>
    public void OnDayNightButtonPressed()
    {
        SettingsScript settings = FindObjectOfType<SettingsScript>();
        if (settings != null)
        {
            settings.ToggleDay();
        }
        else
        {
            Debug.LogWarning("DayNightTogglePowerup: No SettingsScript found in scene.");
        }

        if (popUpWindow != null)
        {
            popUpWindow.SetActive(false);
        }
    }
}
