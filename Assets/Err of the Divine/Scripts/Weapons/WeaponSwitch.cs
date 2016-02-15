using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {

    public GameObject[] weapons;
    public GameObject primary, secondary, third;
	// Use this for initialization
	void Start () {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        DisableWeapons();

        foreach(GameObject weapon in weapons) {
            if(weapon.name == "Handgun") {
                secondary = weapon;
            } else if (weapon.name == "Gladius") {
                primary = weapon;
            }
            else if (weapon.name == "Cestus") {
                third = weapon;
            }
        }

        primary.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        SwitchWeapon();
	}

    void SwitchWeapon() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            DisableWeapons();
            //weapons[0].SetActive(true);
            primary.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            DisableWeapons();
            //weapons[1].SetActive(true);
            secondary.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha3)) {
            DisableWeapons();
            third.SetActive(true);
        }
    }

    void DisableWeapons() {
        foreach(GameObject weapon in weapons) {
            weapon.GetComponent<WeaponBehavior>().Reinitialize();
            weapon.SetActive(false);
        }
    }
}
