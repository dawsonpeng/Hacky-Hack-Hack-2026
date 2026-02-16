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

    private void OnTriggerEnter2D(Collider2D other)
    {
        ReadCoinData(other.gameObject);
    }

    private void ReadCoinData(GameObject coin)
    {
        Powerup(coin);
    }
}
