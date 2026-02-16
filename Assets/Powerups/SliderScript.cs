using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public bool hasPowerup;
    public int lowestValue;
    public int highestValue;

    public void OnCoinPowerup(powerupScript powerup)
    {
        if (powerup == null)
        {
            return;
        }

        hasPowerup = true;
        lowestValue = powerup.lowestValue;
        highestValue = powerup.highestValue;
        Debug.Log($"Slider powerup received: {lowestValue}-{highestValue}");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
