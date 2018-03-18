using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoorControl : MonoBehaviour {

    private WPoint wayPoint;

    private Door doorScript;

    private GameObject overlayUIPrefab;

    void Start(){
        GameObject door = GameObject.Find("MazeEntranceDoor");
        doorScript = door.GetComponent<Door>();
        wayPoint = GetComponent<WPoint>();
    }

    // Update is called once per frame
    void Update() {
        if (wayPoint.IsOccupied()) {
            doorScript.Lock();
        }
    }
}
