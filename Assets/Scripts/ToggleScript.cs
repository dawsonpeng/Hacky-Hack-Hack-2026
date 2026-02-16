using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public bool hasPowerup;
    public bool isActive;

    public void OnCoinPowerup(CoinPowerup coinPowerup)
    {
        if (coinPowerup == null)
        {
            return;
        }

        hasPowerup = true;
        isActive = coinPowerup.isActive;
        Debug.Log($"Toggle powerup received: isActive={isActive}");
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
