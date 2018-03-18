using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    [SerializeField]
    private GameObject wayPointsGuidePrefab;

    public void OnPointerClicked() {
        // Instantiate(wayPointsPrefab, new Vector3(-4.495389f, 2.3f, 69.8f), Quaternion.identity);           // TODO: Have to remove the hardcoded value
        Instantiate(wayPointsGuidePrefab, new Vector3(2.72f, 3.82f, 130.87f), Quaternion.identity);
        Destroy(gameObject);
    }

}
