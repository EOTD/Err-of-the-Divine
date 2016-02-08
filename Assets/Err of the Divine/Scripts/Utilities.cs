using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

    public static void AdjustHealth(GameObject target, float damage) {
        switch (target.gameObject.tag) {
            case "Player":
                target.GetComponent<Player>().stat.playerHealth += damage;
                break;
            case "Enemy":
                //
                break;
            default:
                break;
        }
    }

    public static Mob GetMobData(string name) {
        return MobDatabase.Instance.DB[name];
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
        return SkillDatabase.Instance.DB[id];
    }

    public static Item GetItemData(uint id) {
        return ItemDatabase.Instance.ItemDB[id];
    }

    public static Weapon GetWeaponData(uint id) {
        return ItemDatabase.Instance.WeaponDB[id];
    }

}
