using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleDoorScript : MonoBehaviour {

    [SerializeField]
    private AudioClip templeDoorOpeningClip;

    public void OnTempleDoorClicked() {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("openTrigger");
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.clip == null) {                     // it ensures that it is always played once
            audioSource.clip = templeDoorOpeningClip;
            audioSource.Play();
        }        
    }
}
