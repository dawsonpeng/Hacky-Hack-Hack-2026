using UnityEngine;

/// <summary>Attach to the Zoom Power Up object. Assign Pop Up Window to the Zoom Pop Up. Touch shows the popup.</summary>
public class ZoomPowerupScript : powerupScript
{
    protected override void Awake()
    {
        base.Awake();
        isSlider = true; // so player passes to SliderScript if needed; popup still shows via base.OnCollected
    }
}
