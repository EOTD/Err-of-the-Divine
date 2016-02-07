using UnityEngine;
using System.Collections;

public class MenuAction : MonoBehaviour {

    // Creating a Singleton instance of this script to allow the MenuManager to send commands.
    private static MenuAction instance = null;
    public static MenuAction Instance {
        get { return instance; }
    }

    MenuManager manager;
    Menu menu;

    //ENT_GameManager game;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        manager = MenuManager.Instance;

        //		menu = GameObject.Find ("Menu2").GetComponent<Menu> ();
        //Debug.Log (this.GetComponent<PieMenu> ().commands.Count);
    }
    void Start() {

        /*game = GameManager.Instance;*/
    }

    public void InitCommand(string command) {
        // Debug.Log("Command has been issued " + command);
        //		for (int i=0; i < pie.commands.Count; i++) {
        //			//Debug.Log (pie.commands[i]);
        //			if (command == pie.commands[i]) {
        //Debug.Log ("You've opened your "+ pie.commands[i]);
        //this.gameObject.SendMessage (pie.commands[i], command, SendMessageOptions.DontRequireReceiver);
        //				break;
        //			}
        //		}
        switch (command) {
            case "Feronia":
                Debug.Log("You've changed your God Type to " + command + ".");
                break;
            case "Mars":
                Debug.Log("You've changed your God Type to " + command + ".");
                break;
            case "Jupiter":
                Debug.Log("You've changed your God Type to " + command + ".");
                break;
            case "Mercury":
                Debug.Log("You've changed your God Type to " + command + ".");
                break;
            case "Invalid":
                Debug.LogWarning("The command you've attempted to call is invalid: Command has not been assigned.");
                break;
            default:
                Debug.LogWarning("Attempted to call '" + command + "': Command has not been registered.");
                break;
        }
    }

    //	void Action( string command){
    //		switch (command) {
    //		case "Inventory":
    //			Debug.Log ("You've opened your "+ command+ " window."); break;
    //		case "Settings":
    //			Debug.Log ("You've opened your "+ command+ " window."); break;
    //		}
    //	}

}
