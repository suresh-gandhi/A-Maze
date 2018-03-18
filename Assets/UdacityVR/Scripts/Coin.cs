using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    //Create a reference to the CoinPoofPrefab
    [SerializeField]
    private GameObject poof;

    [SerializeField]
    [Tooltip("The speed of rotation of the Coin")]
    private float rotationSpeed = 240f;
    [SerializeField]
    [Tooltip("The amplitude of SHM of the Coin")]
    private float amplitude = 0.15f;
    [SerializeField]
    [Tooltip("The frequency of SHM of the Coin")]
    private float dippingSpeed = 5.0f;

    private Vector3 basePosition;

    [SerializeField]
    [Tooltip("Coin shall communicate with it and tell that it is collected!")]
    private GameObject displayManager;

    void Start()
    {
        basePosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        Vector3 deltaPosition = new Vector3(0, Mathf.Sin(Time.time * dippingSpeed) * amplitude, 0);
        transform.position = basePosition + deltaPosition;
    }

    public void OnCoinClicked() {
        // Instantiate the CoinPoof Prefab where this coin is located
        Instantiate(poof, basePosition, Quaternion.Euler(270.0f, 0f, 0f));
        // Make sure the poof animates vertically

        // to communicate it to display manager which communicates to the UI eventually
        displayManager.GetComponent<DisplayManager>().UpdateCoinsCollected();

        // Destroy this coin. Check the Unity documentation on how to use Destroy
        Destroy(gameObject);
    }

}
