using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {

    private string interactionText = "";

    [SerializeField]
    private GameObject interactionUICanvas;

    bool isInteractionGoingOn;

    string textString;

    private Text textComponent;

	// Use this for initialization
	void Start () {
        textString = "";
        isInteractionGoingOn = false;
    }

    public void SetInteractionText(string text) {
        if (!isInteractionGoingOn)
        {
            isInteractionGoingOn = true;
            interactionUICanvas.transform.GetChild(0).gameObject.SetActive(true);
            textComponent = interactionUICanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            interactionText = text;
            StartCoroutine(AnimateTextAppearance());

        }
        else {
            // do nothing :)
        }
    }

	// Update is called once per frame
	void Update () {
        if (textComponent != null) {
            textComponent.text = textString;
        }
	}

    IEnumerator AnimateTextAppearance() {
        for (int i = 0; i < interactionText.Length; i++) {
            textString += interactionText[i];
            yield return null;               // write them in the inspector. dont hard code them
        }

        yield return new WaitForSeconds(1.2f);

        for (int j = interactionText.Length - 1; j >= 0; j--) {
            textString = interactionText.Substring(0, j);
            yield return null;               // write them in the inspector. dont hard code them
        }

        GameObject.FindGameObjectWithTag("InteractionUI").transform.GetChild(0).gameObject.SetActive(false);
        textComponent = null;
        isInteractionGoingOn = false;
    }

}
