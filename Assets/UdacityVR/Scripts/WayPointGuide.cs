using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGuide : MonoBehaviour {

    private Vector3 destroyerPosition;              // has to be soft coded later on...

    void Start() {
        destroyerPosition = new Vector3(-0.03f, 4.6f, 129.98f);
    }

	// Update is called once per frame
	void Update () {
        if (Camera.main.transform.parent.transform.position == destroyerPosition) {
            Destroy(gameObject);
        }
	}
}
