using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "haveKeys" that can be checked in OnDoorClicked()
    [SerializeField]
    private bool haveKeys;

    // Create a boolean value called "opening" that can be checked in Update() 
    [SerializeField]
    private bool opening;

    Animator anim;

    AudioSource audioSource;

    [SerializeField]
    private AudioClip clip_locked;

    [SerializeField]
    private AudioClip clip_opening;

    [SerializeField]
    private AudioClip clip_closing;

    [SerializeField]
    private GameObject dManager;

    void Start() {
        anim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = null;
    }

    void Update() {
        // If the door is opening and it is not fully raised
        if (opening) {
            // Animate the door opening
            anim.SetBool("openTheDoor", true);
            print("Inside the update");
            if (audioSource.clip != clip_opening) {
                print("Inside if");
                audioSource.clip = clip_opening;
                audioSource.Play();
            }
        }
    }

    /*
    void OpenTheDoor() {
        anim.SetBool("openTheDoor", true);
        audioSource.clip = clip_opening;
        audioSource.Play();
    }
    */

    //  If the door is clicked then this function is called
    public void OnDoorClicked() {
        // we go inside if only when the door is unlocked. In other words we have the key with us.
        if (haveKeys)
        {
            print("We have the key with us, opening is set to true");
            // Set the "opening" boolean to true
            opening = true;
        }

        //else if(anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MazeEntranceOpening")  )
        else if(   !(   anim.GetBool("openTheDoor")  &&  !anim.GetBool("hasDoorBeenOpened")  )  )
        {
            // print("The door is locked and the sound is playing");
            // print("Hello BOY GLA GLA");
            if (!anim.GetBool("hasDoorBeenOpened")) {            // If the door has not already been opened i.e. it came through this path.
                print("Hello World!");
                dManager.GetComponent<DisplayManager>().ExitDoorIsClickedInLockedState();
            }
            audioSource.clip = clip_locked;
            audioSource.Play();
        }
    
    }

    public void OnDoorPointed() {
        if (haveKeys == true && opening == false) {         // the very initial state basically at which we want to show the hint.
            dManager.GetComponent<DisplayManager>().EntranceDoorIsPointed();
        }
    }

    //This will be only called through keys script
    public void Unlock()
    {
        haveKeys = true;
    }

    public void Lock() {
        anim.SetBool("openTheDoor", false);
        anim.SetBool("hasDoorBeenOpened", true);
        haveKeys = false;
        opening = false;
        if (audioSource.clip == clip_opening) {
            audioSource.clip = clip_closing;
            audioSource.Play();
        }
    }

}
