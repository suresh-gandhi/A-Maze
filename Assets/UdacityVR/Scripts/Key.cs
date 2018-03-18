using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
    [SerializeField]
    private GameObject poof;

    [SerializeField]
    [Tooltip("Door script component of the exit door")]
    private Door exitDoor;

    private bool collected;

    [SerializeField]
    [Tooltip("The speed of rotation of the key")]
    private float rotationSpeed = 240f;
    [SerializeField]
    [Tooltip("The amplitude of SHM of the key")]
    private float amplitude = 0.15f;
    [SerializeField]
    [Tooltip("The frequency of SHM of the key")]
    private float dippingSpeed = 5.0f;

    private Vector3 basePosition;

    [SerializeField]
    [Tooltip("It communicates to the displayManager to send messages for updating the UI")]
    private GameObject displayManager;

    void Start() {
        basePosition = transform.position;
    }

	void Update()
	{
        if (!collected)
        {
            //play rotating and levitating animation
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
            Vector3 deltaPosition = new Vector3(0, Mathf.Sin(Time.time * dippingSpeed) * amplitude, 0);
            transform.position = basePosition + deltaPosition;
        }

	}

	public void OnKeyClicked()
	{
        // Instatiate the KeyPoof Prefab where this key is located
        Instantiate(poof, basePosition, Quaternion.identity);
        
        // Call the Unlock() method on the Door
        exitDoor.Unlock();

        // communicate to the display manager which eventually communicates to the UI
        displayManager.GetComponent<DisplayManager>().KeyIsCollected();
        
        // Set the Key Collected Variable to true
        collected = true;

        // playing the collected sound
        // audioSource.clip = keyPoofClip;
        // audioSource.Play();

        Destroy(gameObject);
    }

}
