using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

    [SerializeField]
    [Tooltip("The overlay UI which needs to be updated accordingly")]
    private GameObject overlayUI;

    
    [SerializeField]
    [Tooltip("The dialog interaction UI manager")]
    private GameObject interactionUIManager;

    [SerializeField]
    [Tooltip("The result UI which has to be displayed on the treasure being collected.")]
    private GameObject resultUI;

    private GameObject oUI;


    private int coinsCollected;

    private bool instantiated;

    private bool resultUIInstantiated;

    bool isKeyCollected;

    // Use this for initialization
    void Start () {
        instantiated = false;
        coinsCollected = 0;
        isKeyCollected = false;
        resultUIInstantiated = false;
    }

    void Update() {
        Vector3 cameraPositionVector = Camera.main.transform.parent.transform.position;
        // if the position is correct and already not instantiated
        if (cameraPositionVector.z < 109f && !instantiated) {               // hard coded value should be removed later on..
            InstantiateOverlayUI();
        }
    }

    // Entrance door calls this method when it is pointed
    public void EntranceDoorIsPointed() {
        // create the message
        string entranceDoorMessage = "Hmmm...I think I should try clicking on it.";
        // send the message to the UI to display
        interactionUIManager.GetComponent<Interaction>().SetInteractionText(entranceDoorMessage);
    }

    public void ExitDoorIsClickedInLockedState() {
        string exitDoorLockedClickedMessage = "I think it is locked. I should better find the key first!";
        interactionUIManager.GetComponent<Interaction>().SetInteractionText(exitDoorLockedClickedMessage);
    }

    public void InstantiateOverlayUI() {
        oUI = Instantiate(overlayUI, Vector3.zero, Quaternion.identity);
        instantiated = true;
    }

    public void InstantiateResultUI() {
        if (resultUIInstantiated != true) {
            StartCoroutine(helper());
            resultUIInstantiated = true;
        }
    }

    IEnumerator helper()
    {
        yield return new WaitForSeconds(2f);
        GameObject rUI = Instantiate(resultUI, new Vector3(0.45f, 8.51f, -14.81f), Quaternion.identity);        //has to be soft coded later on.

        string timeString = oUI.transform.GetChild(4).gameObject.GetComponent<TimeScript>().GetTimeString();

        rUI.transform.GetChild(2).transform.GetComponent<Text>().text = "Time Taken: " + timeString;
        rUI.transform.GetChild(3).transform.GetComponent<Text>().text = "Coins Collected: " + coinsCollected + "/10";
    }


// gets called whenever we collect coins and updates the UI
    public void UpdateCoinsCollected() {
        coinsCollected++;
        string coinsCollectedString = coinsCollected + " / 10";           // shall not be hardcoded and changed later on.
        // because we dont want to change the prefab thing but the actual gameobject. SILLY MISTAKE POINT
        Text coinText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetComponent<Text>();  // hard coded things should be removed in the end.
        coinText.text = coinsCollectedString;
    }

    // gets called whenever our key is collected and updates the UI
    public void KeyIsCollected() {
        isKeyCollected = true;
        // because we dont want to change the prefab thing but the actual gameobject. SILLY MISTAKE POINT
        Text keyText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(3).GetComponent<Text>(); ;     // hard coded things should be removed in the end here also.
        keyText.text = "Hell Yeah :)";
    }

    // info giving methods
    public int NumberOfCoinsCollected() {
        return coinsCollected;
    }

    // info giving methods
    public bool IsTheKeyCollected(){
        return isKeyCollected;
    }

}
