using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WPoint : MonoBehaviour {

    // there are only three possible states in which our waypoint can be in during the lifetime of the game
    private enum State {
        Idle,
        Focussed,
        Occupied
    }

    [Tooltip("The clip that should get played when we click on the waypoint")]
    public AudioClip click_clip;

    public event Action OnOccupied;

    private AudioSource audioSource;

    // keeps track of which state waypoint is in
    private State _state;

    // for better readability of the code
    private const float NULLITY = 0.0f;

    void Start() {
        _state = State.Idle;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = click_clip;
    }

	// Update is called once per frame
	void Update () {
        bool occupied = Camera.main.transform.parent.transform.position == transform.position;
        // if we were occupied(_state) but not now(!occupied)
        if (!occupied && _state == State.Occupied) {
            //print("going inside the if statement");
            _state = State.Idle;                    // idle can come after 1. occupied as well as 2. focussed
        }
        switch (_state) {
            case State.Idle:
                Idle();
                break;
            case State.Focussed:
                Focussed();
                break;
            case State.Occupied:
                Occupied();
                break;
        }

	}

    // when pointer enters
    public void Enter() {               // focussed can come only after idle state
        if (_state == State.Idle)
        {
            _state = State.Focussed;
        }
    }

    // when pointer leaves
    public void Exit() {
        // if we were focussed then we should be idle now
        // but if we were occupied then we should remain occupied now 
        if (_state == State.Focussed)
        {
            _state = State.Idle;                                // idle can come after 1. focussed as well as 2. occupied
        }
        else {
            // nothing i.e. let the state stay the same(occupied)
        }
    }

    // when pointer clicks
    public void Click()
    {
        if (_state == State.Focussed) {
            _state = State.Occupied;                                                // occupied can come only after focussed
            Camera.main.transform.parent.transform.position = transform.position;
            audioSource.Play();
        }
    }

    // state behaviour at the Idle state
    void Idle() {
        //print("Idle state things are getting executed");
        transform.GetChild(1).transform.gameObject.SetActive(false);     // it should be deactivated always in the idle state 
        transform.localScale = Vector3.one;             // Always(even helps in resetting the things)

    }

    // state behaviour at the Focussed state. 
    // Focussed can only come after Idle so we dont have to worry about the scale here. 
    void Focussed() {
        //print("Focussed state things are getting executed");
        transform.GetChild(1).transform.gameObject.SetActive(true);     // it should be activated always in the focussed state
    }

    // state behaviour at the Occupied state
    void Occupied() {
        //print("Occupied State things are geting executed");
        // make the WPoint disappear so that we simply cant interact with it as it doesn't make sense
        transform.localScale = Vector3.one * NULLITY;
        OnOccupied();
    }

    public bool IsOccupied()
    {
        if (_state == State.Occupied)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
