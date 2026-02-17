using UnityEngine;

public class TickSpeedScript : MonoBehaviour
{
    public GameObject tickSpeedPopUp;

    void OnTriggerEnter(Collision collision) {
        Debug.Log("collision detected");
        TriggerEnterFunctions();
    }

    // functions for when we enter the trigger
    public void TriggerEnterFunctions() {
        tickSpeedPopUp.SetActive(true);
    }
}
