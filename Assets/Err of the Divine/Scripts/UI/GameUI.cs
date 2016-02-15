using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

	//// Use this for initialization
	//void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}

    void OnGUI() {
        GUI.TextField(new Rect(Screen.width*0.03f, Screen.height - 50, 50f, 40),"Yuima");

        GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 50, 150f, 20), "Divinity: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerDivinity );
        GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 30, 150f, 20), "Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerHealth);

        GUI.TextField(new Rect(Screen.width * 0.03f, Screen.height - 200, 50f, 150), "\n g \n o \n d \n\n t \n y \n p \n e");
    }
}
