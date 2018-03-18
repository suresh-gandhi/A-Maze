using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour {

    private AudioSource aSource;

    [SerializeField]
    [Tooltip("Background music that should be played at the start of the game")]
    private AudioClip startSurroundings;

    [SerializeField]
    [Tooltip("The maze background sound that should be played")]
    private AudioClip mazeBackground;

    [SerializeField]
    [Tooltip("Background music that should be played at the end of the game")]
    private AudioClip endSurroundings;

    [SerializeField]
    private GameObject startWayPoint;

    [SerializeField]
    private GameObject endWayPoint;

	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
        aSource.clip = startSurroundings;
        aSource.Play();
        startWayPoint.GetComponent<WPoint>().OnOccupied += PlayMazeBackground;
        endWayPoint.GetComponent<WPoint>().OnOccupied += PlayEndSurroundings;
	}

    void PlayMazeBackground() {
        /*
        if (aSource.clip != mazeBackground) {
            aSource.clip = mazeBackground;
            aSource.Play();
        }
        */
        aSource.volume = 0.0075f;
    }

    void PlayEndSurroundings(){
        /*
        if (aSource.clip != endSurroundings){
            aSource.clip = endSurroundings;
            aSource.Play();
        }
        */
        aSource.volume = 1f;
    }

}
