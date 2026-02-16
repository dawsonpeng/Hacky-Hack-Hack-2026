using UnityEngine;

public class powerupScript : MonoBehaviour
{
    public bool isSlider;
    public bool isActive;
    public int lowestValue;
    public int highestValue;

    public void Powerup(GameObject coin)
    {
        if (coin == null)
        {
            return;
        }

        CoinPowerup coinPowerup = coin.GetComponent<CoinPowerup>();
        if (coinPowerup == null)
        {
            return;
        }

        isSlider = coinPowerup.isSlider;
        lowestValue = coinPowerup.lowestValue;
        highestValue = coinPowerup.highestValue;
        isActive = coinPowerup.isActive;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ReadCoinData(other.gameObject);
    }

    private void ReadCoinData(GameObject coin)
    {
        Powerup(coin);
    }
}
