using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public List<Rect> iconButtons = new List<Rect>();

    /*private AudioSource audioSource;
    private AudioSource audioSource2;

    private CameraLook playerCamera;
    private PlayerControl playerMovement;
    */


    [HideInInspector]
    public bool
        played;
    [HideInInspector]
    public bool
        available; // Bool that checks when the command is allowed to be sent.
    MenuAction selected; // The selected item, ready to send the command.
    [HideInInspector]
    public bool
        isOpen; // Bool to check so Hide and Show Coroutines won't overlap
    private float time; // The current time until isOpen is reset
    private float waitTime = 0.5f; // The maximum time that will reset isOpen

    //[HideInInspector]
    public List<Menu> display = new List<Menu>(); // List of Menus that are being displayed ( Maximum 1 anyways ).

    // Set screen size to scale icons
    private static float virtualX = 1920.0f;
    private static float virtualY = 1080.0f;
    private static Vector3 screenSize = new Vector3(Screen.width / virtualX, Screen.height / virtualY, 1.0f);

    // Singleton instance that can be accessed from other scripts.
    private static MenuManager instance = null;
    public static MenuManager Instance {
        get {
            if (instance != null)
                return instance;
            GameObject g = GameObject.Find("/MenuManager");
            if (g == null) {
                g = new GameObject(typeof(MenuManager).ToString());
                instance = g.AddComponent<MenuManager>();
                DontDestroyOnLoad(g);
            }
            else {
                instance = g.GetComponent<MenuManager>();
            }
            return instance;
        }
    }

    // Shows the prepares to show te menu
    public void Show(Menu menu) {
        // Checks if theres any Menu item stored in the display list.
        if (display.Contains(menu))
            return;

        // Hides the rest of the windows, only to display the last window opened.
        foreach (Menu m in display) {
            StartCoroutine(_Hide(m));
        }
        // Adds the window to the list to be stored.
        display.Add(menu);

        // Animation to draw the menu window.
        StartCoroutine(_Show(menu));
    }

    public void Hide(Menu menu) {
        StartCoroutine(_Hide(menu));
    }

    private IEnumerator _Hide(Menu menu) {
        available = false;
        // Player Control Enable here
        /*playerCamera.enabled = true;
        playerMovement.speed = 7f;
        playerMovement.turnSpeed = 3f;
        playerMovement.enabled = true;*/
        while (menu.scale > 0) {
            yield return new WaitForEndOfFrame();
            menu.scale -= Time.deltaTime * menu.speed;
            menu.angle = (1 - menu.scale) * 30f;
        }
        menu.scale = 0f;
        menu.angle = 0f;
        display.Remove(menu);
    }

    private IEnumerator _Show(Menu menu) {
       /* audioSource2.clip = menu.openSound;
        audioSource2.Play();

        // Player Control Disable here
        playerCamera.enabled = false;
        //			playerMovement.speed = 0f;
        //			playerMovement.turnSpeed = 0f;
        playerMovement.enabled = false;
        */

        available = true;
        Screen.lockCursor = true;
        while (menu.scale < 1) {
            yield return new WaitForEndOfFrame();
            menu.scale += Time.deltaTime * menu.speed;
            menu.angle = (1 - menu.scale) * 30f;
        }
        menu.scale = 1f;
        menu.angle = 0f;
        Screen.lockCursor = false;
    }

    void Awake() {
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource2 = gameObject.AddComponent<AudioSource>();
        //playerCamera = GameObject.Find("ENT_MAIN_CAMERA_RIG").GetComponent<ENT_CameraLook>();
        //playerMovement = GameObject.Find("ENT_Topher").GetComponent<ENT_PlayerControl>();

    }

    //		void OnLevelWasLoaded(int level){
    //			
    //			playerCamera = GameObject.Find ("ENT_MAIN_CAMERA_RIG").GetComponent<ENT_CameraLook> ();
    //			playerMovement = GameObject.Find ("ENT_Topher").GetComponent<ENT_PlayerControl> ();
    //
    //		}

    void OnGUI() {
        Event t = Event.current;
        // Checks if a window is already opened.
        CheckOpen();


        // Draws all the menu in the display list.
        foreach (Menu menu in display.ToArray()) {
            DrawMenu(menu);

            // Find Sound Position
            //			int position = 0;
            //			for (int i=0; i<menu.sounds.Length; i++) {
            //				if (GUI.tooltip == menu.commands [i]) {
            //					position = i;
            //					Debug.Log (position);
            //					break;
            //				}
            //			}


            // Find Rect size/position to be used as a hover.
            int position = 0;
            for (int i = 0; i < iconButtons.Count; i++) {
                if (iconButtons[i].Contains(t.mousePosition)) {
                    position = i;
                    break;
                }
            }

            ///* Enable hover sounds here

            // Checks if the amount of button is the same as the icon amount before proceeding.
            // Buttons take a few miliseconds to create so it'll give an error at the beginning.
            // This Check is to ensure that there will be no errors in registering te sounds;

            if (iconButtons.Count == menu.icons.Count) {
                if (iconButtons[position].Contains(t.mousePosition) && available) {
                    if (!played) {
                        /*audioSource.clip = menu.sounds[position];
                        audioSource.Play();//OneShot (menu.sounds [position]);*/
                        played = true;
                    }
                }
                else
                    played = false;
            }
            //*/

        }
        iconButtons.Clear();
        //Debug.Log (available);

    }

    // Checks if a window is already opened. If it is, then count until it is reset.
    private void CheckOpen() {
        if (isOpen) {
            if (time > waitTime) {
                isOpen = false;
                time = 0;
            }
            time += Time.deltaTime;
        }
    }

    Rect temp;
    // Creating the actual menu window.
    private void DrawMenu(Menu menu) {
        // Setting to allow the mousePosition to be read.
        Event e = Event.current;

        if (menu.scale <= 0)
            return;

        TranslateGUI(Screen.width / 2, Screen.height / 2);
        ScaleGUI(menu.scale);
        RotateGUI (menu.angle);
        float d = (2 * Mathf.PI) / menu.icons.Count;
        float radius = (menu.spacing * menu.icons.Count);
        if (menu.skin != null)
            GUI.skin = menu.skin;
        for (int i = 0; i < menu.icons.Count; i++) {
            float theta = (d * i);
            float ix = (Mathf.Cos(theta) * radius) - (menu.iconSize / 2);
            float iy = (Mathf.Sin(theta) * radius) - (menu.iconSize / 2);

            // Assigning the temporary rect
            if (iconButtons.Count < menu.icons.Count) {
                temp = new Rect(ix, iy, menu.iconSize, menu.iconSize);
                iconButtons.Add(temp);
            }

            GUIContent content = new GUIContent();
            content.image = temp.Contains(e.mousePosition) ? menu.hoverIcons[i] : menu.icons[i]; // Set the hover icons
            content.tooltip = menu.commands[i];
            //content.text = menu.commands[i]; // Set the text next to UI

            // Makes the image bigger.
            if (temp.Contains(e.mousePosition)) {
                temp = new Rect(ix, iy, menu.iconSize + menu.icoSize * menu.iconScale, menu.iconSize + menu.icoSize * menu.iconScale);
                //content.text = string.Empty;
            }

            GUI.Button(temp, content);
            // Checking if the Rect contains the mousePosition and whether it is ready to be selected or not.
            if (temp.Contains(e.mousePosition) && Input.GetMouseButtonUp(2) && available ||
                temp.Contains(e.mousePosition) && Input.GetMouseButtonDown(0) && available ||
                temp.Contains(e.mousePosition) && Input.GetKeyUp(KeyCode.LeftControl) && available
                ) {

                StartCoroutine(_Hide(menu));

                //					audioSource.clip = menu.selectSound;
                //									audioSource.clip = menu.sounds[i];
                //									audioSource.Play();

                // Declare and assigns a command
                string cmd = "Invalid";
                try {
                    cmd = menu.commands[i];
                }
                catch (ArgumentOutOfRangeException) {
                    Debug.LogWarning("Menu commands have not been set. Each Command corresponds to how many Icons there are.");
                }

                // the Singleton instance of MenuAction calls the InitCommand function, calling the command.
                MenuAction.Instance.InitCommand(cmd);

                available = false;
                //Screen.lockCursor = true;
                break;
            }
        }
        //iconButtons.Clear ();

    }

    private static void TranslateGUI(float x, float y) {
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(new Vector3(x, y, 0), Quaternion.identity, Vector3.one);
        GUI.matrix *= m;
    }

    private static void RotateGUI(float a) {
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, a), Vector3.one);
        GUI.matrix *= m;
    }

    private static void ScaleGUI(float s) {
        Matrix4x4 m = new Matrix4x4();
        //m.SetTRS (Vector3.zero, Quaternion.identity, Vector3.one * s);
        m.SetTRS(Vector3.zero, Quaternion.identity, screenSize * s);
        GUI.matrix *= m;
    }

}