using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    Player player;
    public Slider health;
    public Slider divinity;

    private WeaponBehavior weapon;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void FixedUpdate() {
        health.value = player.stat.playerHealth * 0.01f;

        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponBehavior>();


        divinity.maxValue = Utilities.GetWeaponData(weapon.weaponID).ClipSize;
        divinity.value = weapon.clipSize;

        //GUI.TextField(new Rect(Screen.width*0.03f, Screen.height - 50, 50f, 40),"Yuima");

        //GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 50, 150f, 20), "Divinity: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerDivinity );
        //GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 30, 150f, 20), "Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerHealth);


        //GUI.DrawTexture(new Rect(Screen.width/2, Screen.height - 300, Screen.width/2, Screen.height/2), health);


        //GUI.TextField(new Rect(Screen.width * 0.03f, Screen.height - 200, 50f, 150), "\n g \n o \n d \n\n t \n y \n p \n e");
        // Temporary Crosshair
        
    }
    
}
