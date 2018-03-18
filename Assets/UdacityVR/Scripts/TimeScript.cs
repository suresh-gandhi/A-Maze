using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                               // this needs to be imported for accessing the text component and play with other UI's, RTF.

public class TimeScript : MonoBehaviour {

    private float timeInSeconds;
    private float startTime;

    private int seconds;
    private int minutes;
    private int hours;

    private string timeString;

    private Text uiText;


	// Use this for initialization
	void Start () {
        timeInSeconds = 0.0f;
        uiText = GetComponent<Text>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timeInSeconds = Time.time - startTime;             // in every frame we get the time elapsed since the start and we update our variable accordingly
        int seconds = ((int)timeInSeconds) % 60;        // seconds in the range from 0 to 59 inclusive
        int minutes = (((int)timeInSeconds) / 60) % 60;     // minutes in the range from 0 to 59 inclusive
        int hours = ((int)timeInSeconds / 3600);
        timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);     // TODO: I am indirectly setting some constraint here. Mind it! There is a bug to removed here afterwards.
                                                                                                // TODO: Logic needs to be rechecked again, COURTSEY: ROHIT
        uiText.text = timeString;
    }

    public string GetTimeString() {
        return timeString;
    }
}
