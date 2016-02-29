using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

    [SerializeField] public uint weaponID;

    Weapon weapon;

    string weaponName;

    //uint weaponMinDmg; uint weaponMaxDmg; float weaponMultiplier; float weaponRate; float weaponAccuracy; float weaponADS; float weaponRecoil;
    //uint weaponRange; float weaponFallOff; uint weaponClipSize; float weaponReloadSpd;

    private RaycastHit hit;
    private Ray ray;

    // Timer Variables
    private float rateTime;
    private float reloadTime;

    private bool isReloading;

    public uint clipSize;

    public GameObject particle;

    //public Text reloadText;	

	void Start () {
        SetWeaponData();
	}

    void Update() {


        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, 1000f * Time.deltaTime);
        switch(weapon.Type) {
            case WeaponType.Automatic:
                // Shoot Mouse Button
                if (Input.GetMouseButton(0) && Fireable()) {

                    // Set shooting position to the center of the Camera.
                    int x = Screen.width / 2; int y = Screen.height / 2;
                    ray = Camera.main.ScreenPointToRay(new Vector3(x, y));


                    // Calling the weapon function
                    InitiateWeaponBehavior();
                }
                break;



            case WeaponType.Manual:
                if (Input.GetMouseButtonDown(0) && Fireable()) {
                    // Set shooting position to the center of the Camera.
                    int x = Screen.width / 2; int y = Screen.height / 2;
                    ray = Camera.main.ScreenPointToRay(new Vector3(x, y));


                    // Calling the weapon function
                    InitiateWeaponBehavior();
                }
                break;
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
                if (Physics.Raycast(ray, out hit, weapon.Range)) {

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
                if (Physics.Raycast(ray, out hit, weapon.Range)) {

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
            rateTime = weapon.Rate;
        }
        yield return null;
    }

    // Check if item is fireable with the rate of fire. This is just here to make things look fancy.
    private bool Fireable() {
        if (rateTime >= weapon.Rate && CheckAmmo() && !isReloading)
            return true;
        else
            return false;
    }

    private int CalculateDamage() {
        return (int)-Random.Range(weapon.MinDamage, weapon.MaxDamage);
    }

    // This will get the weapon data from the weapon_db.json
    private void SetWeaponData() {

        weapon = Utilities.GetWeaponData(weaponID);
        weaponName = Utilities.GetItemData(weaponID).Name;

        clipSize = weapon.ClipSize;
        rateTime = weapon.Rate;
    }

    private bool CheckAmmo() {
        return clipSize > 0 ? true : false;
    }

    private bool MaximumCapacity() {
        return clipSize >= weapon.ClipSize ? true : false;
    }

    void DecreaseAmmo() {
        if(clipSize > 0) {
            clipSize--;
        }
    }

    private IEnumerator Reload() {
        if (!isReloading) {
            while (reloadTime < weapon.ReloadSpd) {
                isReloading = true;
                reloadTime += Time.deltaTime;
                yield return null;
            }
            if (reloadTime >= weapon.ReloadSpd) {
                reloadTime = 0;
                isReloading = false;
                clipSize = weapon.ClipSize;
            }
        
        }
        yield return null;
    }

    public void Reinitialize() {
        reloadTime = 0;
        rateTime = Utilities.GetWeaponData(weaponID).Rate;
        isReloading = false;
        StopAllCoroutines();
    }

    public Texture2D crosshair;
    
    void OnGUI() {
        // Weapon Information at bottom left of the screen. Name and Ammo
        //if(CheckAmmo())
        //    GUI.TextField(new Rect(Screen.width - 150, Screen.height - 50, 100f, 40),weaponName+" \nAmmo: "+clipSize+" / "+weapon.ClipSize);
        //else
        //if (!CheckAmmo())
        //    reloadText.gameObject.SetActive(true);
        //else
        //    reloadText.gameObject.SetActive(false);
        //    GUI.TextField(new Rect(Screen.width - 150, Screen.height - 50, 100f, 40), weaponName + " \n'R' to Reload");

        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 50f, 50f), crosshair);
    }
}
