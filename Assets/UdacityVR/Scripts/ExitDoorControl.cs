using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorControl : MonoBehaviour {

    private WPoint wayPoint;
 
    private Door doorScript;

    // Use this for initialization
    void Start () {
        GameObject door = GameObject.Find("MazeExitDoor");
        doorScript = door.GetComponent<Door>();
        wayPoint = GetComponent<WPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoint.IsOccupied())
        {
            doorScript.Lock();
        }
    }
}
