using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationScript : MonoBehaviour {

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private Animator anim;

    private Vector3 basePositionCanvas;

	// Use this for initialization
	void Start () {
        basePositionCanvas = new Vector3( 0.03f , 3.46f , 137.46f );
	}
	
	// Update is called once per frame
	void Update () {
        if(anim == null)
        {
            // Do nothing
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("StartSceneAnimationIdle")) {        // 0 stands for base layer
            Destroy(anim);
            Instantiate(canvas, basePositionCanvas , Quaternion.identity);
        }
	}
}
