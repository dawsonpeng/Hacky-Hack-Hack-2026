using UnityEngine;

/// <summary>Put this on the day/night popup root. Button On Click: call ToggleDayAndClose on this popup.</summary>
public class DayNightPopupScript : MonoBehaviour
{
    /// <summary>Call from the popup Button's On Click (). Toggles day/night and closes this popup.</summary>
    public void ToggleDayAndClose()
    {
        SettingsScript settings = FindObjectOfType<SettingsScript>();
        if (settings != null)
        {
            settings.ToggleDay();
        }

        gameObject.SetActive(false);
    }
}
