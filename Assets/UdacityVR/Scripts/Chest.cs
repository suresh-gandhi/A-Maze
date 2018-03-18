using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private GameObject displayManager;

    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("The victory sound which should get played when we finally find the treasure")]
    private AudioClip victorySound;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick()
    {
        anim.SetTrigger("openTrigger");
        displayManager.GetComponent<DisplayManager>().InstantiateResultUI();
        if (audioSource.clip != victorySound) {
            Invoke("PlayVictorySound", 1.5f);
        }
    }

    void PlayVictorySound() {
        audioSource.clip = victorySound;
        audioSource.PlayOneShot(victorySound);
    }

}
