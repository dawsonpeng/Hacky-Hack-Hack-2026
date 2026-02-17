using UnityEngine;

public class VolumePowerUpScript : MonoBehaviour
{
    public GameObject volumePopUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // settings = GameObject.FindWithTag("Settings");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collision collision) {
        Debug.Log("collision detected");
        TriggerEnterFunctions();
    }

    // functions for when we enter the trigger
    public void TriggerEnterFunctions() {
        volumePopUp.SetActive(true);
    }
}
