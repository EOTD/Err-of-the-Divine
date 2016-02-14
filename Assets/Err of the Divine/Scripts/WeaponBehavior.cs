using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

    [SerializeField] private uint weaponID;

    private string weaponName;

    uint weaponMinDmg; uint weaponMaxDmg; float weaponMultiplier; float weaponRate; float weaponAccuracy; float weaponADS; float weaponRecoil;
    uint weaponRange; float weaponFallOff; uint weaponClipSize; float weaponReloadSpd;

    private RaycastHit hit;
    private Ray ray;

	// Use this for initialization
	void Start () {
        SetWeaponData();
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {

            // Set shooting position to the center of the Camera.
            int x = Screen.width / 2; int y = Screen.height / 2;
            ray = Camera.main.ScreenPointToRay(new Vector3(x, y));

            // Getting the weapon function
            InitiateWeaponBehavior();
            //Debug.DrawRay(ray.origin, ray.direction * 30000, new Color(1f, 0f, 0f, 1f));
        }
    }

	// All of the weapon's behavior are located here.
	private void InitiateWeaponBehavior() {

        // We're going to do a seperate case for each individual weapon.
        switch (weaponID) {

            case 2001: // Handgun
                break;

            case 2002: // Gladius
                if (Physics.Raycast(ray, out hit, weaponRange)) {

                    // Get all the Enemy tags that you assigned in the Utilities script.
                    foreach (string tag in Utilities.GetEnemyTags()) {
                        if (hit.collider.tag == tag) {
                            Debug.Log("You hit "+ hit.collider.tag+ " and dealt " + CalculateDamage());
                            Utilities.AdjustHealth(hit.collider.gameObject, CalculateDamage());
                            break;
                        }
                    }
                }
                break;

            case 2003:
                break;
        }

    }

    private int CalculateDamage() {
        return (int)-Random.Range(weaponMinDmg, weaponMaxDmg);
    }

    // This will get the weapon data from the weapon_db.json
    private void SetWeaponData() {
        weaponName = Utilities.GetItemData(weaponID).Name;
        weaponMinDmg = Utilities.GetWeaponData(weaponID).MinDamage;
        weaponMaxDmg = Utilities.GetWeaponData(weaponID).MaxDamage;
        weaponMultiplier = Utilities.GetWeaponData(weaponID).Multiplier;
        weaponRate = Utilities.GetWeaponData(weaponID).Rate;
        weaponAccuracy = Utilities.GetWeaponData(weaponID).Accuracy;
        weaponADS = Utilities.GetWeaponData(weaponID).ADS;
        weaponRecoil = Utilities.GetWeaponData(weaponID).Recoil;
        weaponRange = Utilities.GetWeaponData(weaponID).Range;
        weaponFallOff = Utilities.GetWeaponData(weaponID).FallOff;
        weaponClipSize = Utilities.GetWeaponData(weaponID).ClipSize;
        weaponReloadSpd = Utilities.GetWeaponData(weaponID).ReloadSpd;
    }
}
