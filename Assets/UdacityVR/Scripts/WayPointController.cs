using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour {

    /*
     * 0 0 0 0 0 0 0 0 
     * 0 0 0 0 0 0 0 0
     * 0 0 0 0 0 0 0 0
     * 0 0 0 0 0 0 0 0
     * 0 0 0 0 0 0 0 0
     * 0 0 0 0 0 0 0 0
     */

    [SerializeField]
    private GameObject[] arrayOfWayPoints;

    private int activeWayPointNumber;

    private int forwardWayPointNumber;
    private int backwardWayPointNumber;
    private int leftWayPointNumber;
    private int rightWayPointNumber;

    // private Vector3 basePostion;
 
	// Use this for initialization
	void Start () {
        print("Inside Start");
        activeWayPointNumber = 4;
        forwardWayPointNumber = 12;
        leftWayPointNumber = 3;
        rightWayPointNumber = 5;
        backwardWayPointNumber = -4;

        ActivateWayPoint(activeWayPointNumber);
        ActivateWayPoint(forwardWayPointNumber);
        ActivateWayPoint(backwardWayPointNumber);
        ActivateWayPoint(leftWayPointNumber);
        ActivateWayPoint(rightWayPointNumber);
      
        // initialize base position.
        // basePostion = new Vector3(35.50461f, 4.6f, 104.8f);           // TODO: hard coded thing shall be looked into later.
	}
	
	// Update is called once per frame
	void Update () {
        print("Inside Update");
        Vector3 currentPosition = Camera.main.transform.parent.transform.position;
        if (currentPosition == positionOf(forwardWayPointNumber))
        {
            UpdateWaypoints(forwardWayPointNumber);
        }
        else if (currentPosition == positionOf(backwardWayPointNumber))
        {
            UpdateWaypoints(backwardWayPointNumber);
        }
        else if (currentPosition == positionOf(leftWayPointNumber))
        {
            UpdateWaypoints(leftWayPointNumber);
        }
        else if(currentPosition == positionOf(rightWayPointNumber)) {
            UpdateWaypoints(rightWayPointNumber);
        }
	}

    // calculates the position(Vector3) of the waypoint inside the maze given the waypoint number.
    Vector3 positionOf(int wayPointNumber) {
        /* 
        int columnNumber = wayPointNumber % 8;      
        int rowNumber = wayPointNumber / 8;
        float x = basePostion.x - columnNumber * 10;
        float z = basePostion.z - rowNumber * 10;
        float y = basePostion.y;
        return new Vector3(x, y, z);
        */
        print("Inside positionOf");
        if (IsValidWayPoint(wayPointNumber))
        {
            return arrayOfWayPoints[wayPointNumber].transform.position;
        }
        else {
            return Vector3.zero;
        }
    }

    void UpdateWaypoints(int wayPointNumberToWhichIMove) {
        print("Inside UpdateWaypoints");
        if (wayPointNumberToWhichIMove != forwardWayPointNumber) {      // has not moved in the forward direction
            DeactivateWayPoint(forwardWayPointNumber);
        }
        if (wayPointNumberToWhichIMove != backwardWayPointNumber) {     // has not moved in the backward direction
            DeactivateWayPoint(backwardWayPointNumber);
        }
        if (wayPointNumberToWhichIMove != leftWayPointNumber) {         // has not moved in the left direction
            DeactivateWayPoint(leftWayPointNumber);
        }
        if (wayPointNumberToWhichIMove != rightWayPointNumber) {        // has not moved in the right direction
            DeactivateWayPoint(rightWayPointNumber);
        }

        // update forward, backward, left and right numbers.
        forwardWayPointNumber = wayPointNumberToWhichIMove + 8;
        backwardWayPointNumber = wayPointNumberToWhichIMove - 8;
        leftWayPointNumber = wayPointNumberToWhichIMove - 1;
        rightWayPointNumber = wayPointNumberToWhichIMove + 1;

        // activate the waypoints accordingly.
        ActivateWayPoint(forwardWayPointNumber);
        ActivateWayPoint(backwardWayPointNumber);
        ActivateWayPoint(leftWayPointNumber);
        ActivateWayPoint(rightWayPointNumber);

        activeWayPointNumber = wayPointNumberToWhichIMove;
    }

    void ActivateWayPoint(int wayPointNumber) {
        // print("Inside ActivateWayPoint");
        if (IsValidWayPoint(wayPointNumber)) {
            arrayOfWayPoints[wayPointNumber].SetActive(true);
        }
    }

    void DeactivateWayPoint(int wayPointNumber) {
        // print("Inside DeactivateWayPoint");
        if (IsValidWayPoint(wayPointNumber)) {
            arrayOfWayPoints[wayPointNumber].SetActive(false);
        }
    }

    bool IsValidWayPoint(int wayPointNumber) {
        // print("Inside IsValidWayPoint");
        return wayPointNumber >= 0 && wayPointNumber < arrayOfWayPoints.Length;
    }

}
