using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    Player player;
    public Slider health;
    public Slider ammo;
    public Slider divinity;

    private WeaponBehavior weapon;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Cursor.visible = false;
    }

    void Update() {
        health.value = player.stat.playerHealth * 0.01f;

        foreach (GameObject weap in GameObject.FindGameObjectsWithTag("Weapon")) {
            if(weap.activeInHierarchy)
                weapon = weap.GetComponent<WeaponBehavior>();
        }



        ammo.maxValue = Utilities.GetWeaponData(weapon.weaponID).ClipSize;
        ammo.value = weapon.clipSize;

        divinity.maxValue = Player.Instance.stat.playerDivinity;
        divinity.value = Player.Instance.currentDivinity; 
        
        //GUI.TextField(new Rect(Screen.width*0.03f, Screen.height - 50, 50f, 40),"Yuima");

        //GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 50, 150f, 20), "Divinity: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerDivinity );
        //GUI.TextField(new Rect(Screen.width * 0.09f, Screen.height - 30, 150f, 20), "Health: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stat.playerHealth);


        //GUI.DrawTexture(new Rect(Screen.width/2, Screen.height - 300, Screen.width/2, Screen.height/2), health);


        //GUI.TextField(new Rect(Screen.width * 0.03f, Screen.height - 200, 50f, 150), "\n g \n o \n d \n\n t \n y \n p \n e");
        // Temporary Crosshair
        if (Input.GetKey(KeyCode.Escape)) {
            Cursor.visible = !Cursor.visible;
        }
        
    }

    void OnGUI() {
        if(GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Restart")){
            SceneManager.LoadScene(1);
        }
    }


    
}
