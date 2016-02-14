using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {

    GameObject[] weapons;
	// Use this for initialization
	void Start () {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        DisableWeapons();
        weapons[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        SwitchWeapon();
	}

    void SwitchWeapon() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            DisableWeapons();
            weapons[0].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            DisableWeapons();
            weapons[1].SetActive(true);
        }
    }

    void DisableWeapons() {
        foreach(GameObject weapon in weapons) {
            weapon.GetComponent<WeaponBehavior>().Reinitialize();
            weapon.SetActive(false);
        }
    }
}
