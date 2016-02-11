using UnityEngine;
using System.Collections;

public class DialogueBox : MonoBehaviour {

    DialogueParser parser;

    public string dialogue;
    public string dialogueName;
    public Sprite pose;
    public string position;
    int lineNum;

    public GUIStyle dialogueStyle, dialogueNameStyle;
    // Use this for initialization
    void Start () {
        dialogue = "";
        parser = GameObject.Find("Parser").GetComponent<DialogueParser>();
        lineNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {

            ResetImages();
            dialogueName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            DisplayImages();
            lineNum++;
        }
	
	}

    void ResetImages() {
        if (dialogueName != "") {
            GameObject character = GameObject.Find(name);
            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = null;
        }
    }

    void DisplayImages() {
        if (dialogueName != "") {
            GameObject character = GameObject.Find(name);

            SetSpritePosition(character);

            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
            currSprite.sprite = pose;
        }
    }

    void SetSpritePosition(GameObject spriteObj) {
        switch (position) {
            case "L":
                spriteObj.transform.position = new Vector3(-30, 2, 70);
                break;
            case "R":
                spriteObj.transform.position = new Vector3(20, 2, 70);
                break;

        }
    }

    void OnGUI() {
        dialogueName = GUI.TextField(new Rect(0, 400, 200, 50), dialogueName, dialogueNameStyle);
        dialogue = GUI.TextField(new Rect(0, 450, 700, 200), dialogue, dialogueStyle);
    }
}
