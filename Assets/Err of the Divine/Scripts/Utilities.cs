using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

    public static void AdjustHealth(GameObject target, int damage) {
        switch (target.gameObject.tag) {
            case "Player":
                target.GetComponent<Player>().stat.playerHealth += damage;
                break;
            case "Enemy":
                //
                break;
            case "Mercury":
                target.GetComponent<MercuryAI>().currentHealth += damage;
                break;

            default:
                break;
        }
    }

    public static void InstantiateObjectPool(GameObject obj, int amount){
        for(int i=0; i< amount; i++) {
            Instantiate(obj);
        }
    }

    public static bool InRange(Transform current, Transform target , float distance) {
        return Vector3.Distance(current.transform.position, target.transform.position) <= distance ? true : false;
    }


    /*  ---------------------------- */
    /*    Skills Utility             */
    /* ------------------------------*/
    /* This area is for common uses 
       throughout the project. 
       Almost every enemy will be 
       stunnable. If they aren't
       then their state will not do anything
       -------------------------------*/
    /*  ---------------------------- */
    public static void Stun(GameObject target) {
        switch (target.tag) {
            case "Mercury":
                target.gameObject.GetComponent<MercuryAI>().state = MercuryAI.State.Stunned;
                break;
            case "Malice":
                target.gameObject.GetComponent<MinionAI>().state = MinionAI.State.Stunned;
                break;

        }
    }

    public static Skill GetSkillData(uint id) {
        return Database.Instance.SkillDB[id];
    }

    public static Item GetItemData(uint id) {
        return Database.Instance.ItemDB[id];
    }

    public static Weapon GetWeaponData(uint id) {
        return Database.Instance.WeaponDB[id];
    }

    public static Mob GetMobData(uint id) {
        return Database.Instance.MobDB[id];
    }

    public static string[] GetEnemyTags() {
        return new string[] { "Mercury", "Enemy", "Civillian" };
    }

}
