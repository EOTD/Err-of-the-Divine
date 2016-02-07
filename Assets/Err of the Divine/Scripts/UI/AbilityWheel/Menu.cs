using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {


    //public int MenuID;
    public List<string> commands; // List of commands to send.
    public List<Texture> icons; // List of textures for the icons
    public List<Texture> hoverIcons;

    public float iconSize = 64f; // The size of the icon, scaled.
    public float spacing = 12f; // Space between each individual item.
    public float speed = 8f; // How fast the UI will poop out.
    public GUISkin skin;

    public float iconScale = 3.0f;
    public float icoSize = 10f;

    public ItemType itemType;

    public enum ItemType {
        Weapon, Consumable, Throwable, Quest
    }

    [HideInInspector]
    public float scale; // Scale that will be manipulated for animation.
    [HideInInspector]
    public float angle; // Angle that will be manipulated for rotation animations.

    MenuManager manager; // Used for instancing Singleton of MenuManager.

    public KeyCode key; // Declaring a Keycode to open the window.

    public MouseInput mouseDown;
    public MouseInput mouseUp;

    //public MouseFunctions mouseFunction;
    public enum MouseInput {
        None, LeftMouseDown, LeftMouseUp, RightMouseDown, RightMouseUp, MiddleMouseDown, MiddleMouseUp
    }
    public AudioClip selectSound;
    public AudioClip openSound;
    public AudioClip[] sounds;
    private AudioSource audioSource;


    private bool MouseButton(MouseInput mouse) {
        switch (mouse) {

            case MouseInput.MiddleMouseUp:
                return Input.GetMouseButtonUp(2);
            case MouseInput.MiddleMouseDown:
                return Input.GetMouseButtonDown(2);
            default:
                return false;

        }
    }

    void Start() {
        switch (gameObject.name) {
            case "Abilities":
                AddMenu("Feronia");
                AddMenu("Mercury");
                AddMenu("Mars");
                AddMenu("Jupiter");
                break;
        }

        // Calculate spacing so it's equal with all other window
        spacing = (iconSize / icons.Count) * 2;
    }

    void AddMenu(string name) {
        commands.Add(name); // List of commands to send.
        icons.Add(Resources.Load<Texture2D>("Abilities/" + name)); // List of textures for the icons
        hoverIcons.Add(Resources.Load<Texture2D>("Abilities/" + name + "-hl"));
    }

    void Awake() {

        // Singleton of MenuManager instance.
        manager = MenuManager.Instance;
        audioSource = GetComponent<AudioSource>();

    }

    void Update() {

        if (key != KeyCode.None) {

            if (Input.GetKeyDown(key) && !manager.isOpen) {
                if (manager.available) {
                    /*audioSource.clip = selectSound;
                    audioSource.Play();*/

                }
                //audioSource.Play ();
                foreach (Menu h in manager.display)
                    manager.Hide(h);
                manager.Show(this);
                manager.isOpen = true;
            }
            else if (Input.GetKeyUp(key) && !manager.isOpen) {
                foreach (Menu h in manager.display)
                    manager.Hide(h);
            }

        }
        else {

            if (MouseButton(mouseDown) && !manager.isOpen) {
                if (manager.available) {
                   /* audioSource.clip = selectSound;
                    audioSource.Play();*/
                }
                foreach (Menu h in manager.display)
                    manager.Hide(h);
                manager.Show(this);
                manager.isOpen = true;
            }
            else if (MouseButton(mouseUp) && !manager.isOpen) {
                foreach (Menu h in manager.display)
                    manager.Hide(h);
            }

        }

    }

}