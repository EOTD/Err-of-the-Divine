using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

    [SerializeField] private uint weaponID;

    string weaponName;

    uint weaponMinDmg; uint weaponMaxDmg; float weaponMultiplier; float weaponRate; float weaponAccuracy; float weaponADS; float weaponRecoil;
    uint weaponRange; float weaponFallOff; uint weaponClipSize; float weaponReloadSpd;

    private RaycastHit hit;
    private Ray ray;

    // Timer Variables
    private float rateTime;
    private float reloadTime;

    private bool isReloading;

    private uint clipSize;

    public GameObject particle;

	
	void Start () {
        SetWeaponData();
	}

    void Update() {


        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, 1000f * Time.deltaTime);

        // Shoot Mouse Button
        if (Input.GetMouseButton(0) && Fireable()) {

            // Set shooting position to the center of the Camera.
            int x = Screen.width / 2; int y = Screen.height / 2;
            ray = Camera.main.ScreenPointToRay(new Vector3(x, y));


            // Calling the weapon function
            InitiateWeaponBehavior();
        }

        // Reload Key R
        if (Input.GetKeyDown(KeyCode.R)) {
            if (!MaximumCapacity()) {
                StartCoroutine(Reload());
            }
        }

    }

    // All of the weapon's behavior are located here.
    private void InitiateWeaponBehavior() {


        StartCoroutine(DecreaseRateTimer());

        // We're going to do a seperate case for each individual weapon.

        switch (weaponID) {

            case 2001: // Handgun
                if (Physics.Raycast(ray, out hit, weaponRange)) {

                    // Get all the Enemy tags that you assigned in the Utilities script.
                    foreach (string tag in Utilities.GetEnemyTags()) {
                        if (hit.collider.tag == tag) {
                            GameObject hitObj = hit.collider.gameObject;
                            Utilities.AdjustHealth(hitObj, CalculateDamage());
                            break;
                        }
                    }
                    // Get the Raycast hit position and rotation to Instantiate the particle.
                    EmitParticle(hit.point, Quaternion.LookRotation(hit.normal));
                }
                break;

            case 2002: // Gladius
                if (Physics.Raycast(ray, out hit, weaponRange)) {

                    // Get all the Enemy tags that you assigned in the Utilities script.
                    foreach (string tag in Utilities.GetEnemyTags()) {
                        if (hit.collider.tag == tag) {
                            GameObject hitObj = hit.collider.gameObject;
                            Utilities.AdjustHealth(hitObj, CalculateDamage());
                            break;
                        }
                    }
                    // Get the Raycast hit position and rotation to Instantiate the particle.
                    EmitParticle(hit.point, Quaternion.LookRotation(hit.normal));
                }
                break;

            case 2003:
                break;
        }
        DecreaseAmmo();

    }

    // Creates the particle when fired.
    void EmitParticle(Vector3 pos, Quaternion rot) {
        GameObject par = Instantiate(particle, pos, Quaternion.identity) as GameObject;
        Destroy(par, 1f);
    }

    // Calculate the Rate of Fire here
    private IEnumerator DecreaseRateTimer() {
        while (rateTime > 0) {
            rateTime -= Time.deltaTime;
            yield return null;
        }
        if (rateTime <= 0) {
            rateTime = weaponRate;
        }
        yield return null;
    }

    // Check if item is fireable with the rate of fire. This is just here to make things look fancy.
    private bool Fireable() {
        if (rateTime >= weaponRate && CheckAmmo() && !isReloading)
            return true;
        else
            return false;
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

        clipSize = weaponClipSize;
        rateTime = weaponRate;
    }

    private bool CheckAmmo() {
        return clipSize > 0 ? true : false;
    }

    private bool MaximumCapacity() {
        return clipSize >= weaponClipSize ? true : false;
    }

    void DecreaseAmmo() {
        if(clipSize > 0) {
            clipSize--;
        }
    }

    private IEnumerator Reload() {
        if (!isReloading) {
            while (reloadTime < weaponReloadSpd) {
                isReloading = true;
                reloadTime += Time.deltaTime;
                yield return null;
            }
            if (reloadTime >= weaponReloadSpd) {
                reloadTime = 0;
                isReloading = false;
                clipSize = weaponClipSize;
            }
        
        }
        yield return null;
    }

    public void Reinitialize() {
        reloadTime = 0;
        rateTime = weaponRate;
        isReloading = false;
        StopAllCoroutines();
    }


    void OnGUI() {
        // Weapon Information at bottom left of the screen. Name and Ammo
        if(CheckAmmo())
            GUI.TextField(new Rect(Screen.width - 150, Screen.height - 50, 100f, 40),weaponName+" \nAmmo: "+clipSize+" / "+weaponClipSize);
        else
            GUI.TextField(new Rect(Screen.width - 150, Screen.height - 50, 100f, 40), weaponName + " \n'R' to Reload");

        // Temporary Crosshair
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10f, 10f), "x");
    }
}
