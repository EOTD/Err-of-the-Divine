using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class Database : MonoBehaviour {

    private string itemFile = "item_db.json";
    private string weaponFile = "weapon_db.json";
    private string mobFile = "mob_db.json";
    private string skillFile = "skill_db.json";
    private string fileDirectory = "/Err of the Divine/Resources/Database/";

    [Header("Item Database")]
    [SerializeField] private List<Item> ITEM_DATABASE = new List<Item>();
    public Dictionary<uint, Item> ItemDB = new Dictionary<uint, Item>();
    private JsonData itemData; // Stores text data to use for the item database.

    [Header("Weapon Data")]
    [SerializeField] private List<Weapon> WEAPON_DATABASE = new List<Weapon>();
    public Dictionary<uint, Weapon> WeaponDB = new Dictionary<uint, Weapon>();
    private JsonData weaponData; // Stores text data to use for the weapon database.

    [Header("Skill Database")]
    [SerializeField] private List<Skill> SKILL_DATABASE = new List<Skill>();
    public Dictionary<uint, Skill> SkillDB = new Dictionary<uint, Skill>();
    private JsonData skillData; // Stores text data to use for the weapon database.

    [Header("Mob Database")]
    [SerializeField] private List<Mob> MOB_DATABASE = new List<Mob>();
    public Dictionary<uint, Mob> MobDB = new Dictionary<uint, Mob>();
    private JsonData mobData; // Stores text data to use for the weapon database.
    
    private static Database instance;
    public static Database Instance { get { return instance; } set { instance = value; } }

    void Awake() {
        if (instance == null)
            instance = this;

        BuildItemDatabase();
        BuildWeaponDatabase();
        BuildSkillDatabase();
        BuildMobDatabase();
        StoreDatabase();
    }

    private void BuildItemDatabase() {
        // ID - Name - Description - Type - Quality - Equipable - Stackable
        uint iID; string iName; string iDesc; uint iType; uint iQuality; uint iEquipable; uint iStackable;

        itemData = GetTextData(itemFile);

        for (int i = 0; i < itemData.Count; i++) {

            iID = uint.Parse(itemData[i]["id"].ToString());
            iName = itemData[i]["name"].ToString();
            iDesc = itemData[i]["desc"].ToString();
            iType = uint.Parse(itemData[i]["type"].ToString());
            iQuality = uint.Parse(itemData[i]["quality"].ToString());
            iEquipable = uint.Parse(itemData[i]["equipable"].ToString());
            iStackable = uint.Parse(itemData[i]["stackable"].ToString());

            ITEM_DATABASE.Add(new Item(iID, iName, iDesc, iType, iQuality, iEquipable, iStackable));
        }
    }

    private void BuildWeaponDatabase() {
        // ID - Type - Min - Max - Multiplier - Rate - Accuracy - ADS - Recoil - Range - Falloff Rate - Clip Size - Reload Speed
        weaponData = GetTextData(weaponFile);

        for (int i = 0; i < weaponData.Count; i++) {

            uint wID = uint.Parse(weaponData[i]["id"].ToString());
            uint wType = uint.Parse(weaponData[i]["type"].ToString());
            uint wMin = uint.Parse(weaponData[i]["min"].ToString());
            uint wMax = uint.Parse(weaponData[i]["max"].ToString());
            float wMultiplier = float.Parse(weaponData[i]["multiplier"].ToString());
            float wRate = float.Parse(weaponData[i]["rate"].ToString());
            float wAccuracy = float.Parse(weaponData[i]["accuracy"].ToString());
            float wADS = float.Parse(weaponData[i]["ads"].ToString());
            float wRecoil = float.Parse(weaponData[i]["recoil"].ToString());
            uint wRange = uint.Parse(weaponData[i]["range"].ToString());
            float wFalloff = float.Parse(weaponData[i]["falloff"].ToString());
            uint wClipSize = uint.Parse(weaponData[i]["clipsize"].ToString());
            float wReloadSpd = float.Parse(weaponData[i]["reloadspd"].ToString());

            WEAPON_DATABASE.Add(new Weapon(wID, wType, wMin, wMax, wMultiplier, wRate, wAccuracy, wADS, wRecoil, wRange, wFalloff, wClipSize, wReloadSpd));
        }
    }

    private void BuildSkillDatabase() {
        // ID - Name - Ability - Type - Cost - AoE - Range - Cooldown
        skillData = GetTextData(skillFile);

        for (int i=0; i < skillData.Count; i++) {
            uint sID = uint.Parse(skillData[i]["id"].ToString());
            string sName = skillData[i]["name"].ToString();
            uint sAbility = uint.Parse(skillData[i]["ability"].ToString());
            uint sType = uint.Parse(skillData[i]["type"].ToString());
            uint sCost = uint.Parse(skillData[i]["cost"].ToString());
            uint sAoe = uint.Parse(skillData[i]["aoe"].ToString());
            uint sRange = uint.Parse(skillData[i]["range"].ToString());
            float sCooldown = float.Parse(skillData[i]["cooldown"].ToString());

            SKILL_DATABASE.Add(new Skill(sID,sName,sAbility,sType,sCost,sAoe,sRange,sCooldown));
        }
    }

    private void BuildMobDatabase() {
        // ID - Name - Type - Level - Health - Divinty - Atk1 - Atk2 - Atk3 - Drop1 - Drop2
        mobData = GetTextData(mobFile);

        for (int i = 0; i < mobData.Count; i++) {
            uint mID = uint.Parse(mobData[i]["id"].ToString());
            string mName = mobData[i]["name"].ToString();
            uint mType = uint.Parse(mobData[i]["type"].ToString());
            uint mLevel = uint.Parse(mobData[i]["level"].ToString());
            uint mHealth = uint.Parse(mobData[i]["health"].ToString());
            uint mDivinity = uint.Parse(mobData[i]["divinity"].ToString());
            uint mAtk1 = uint.Parse(mobData[i]["atk1"].ToString());
            uint mAtk2 = uint.Parse(mobData[i]["atk2"].ToString());
            uint mAtk3 = uint.Parse(mobData[i]["atk3"].ToString());
            uint mDrop1 = uint.Parse(mobData[i]["drop1"].ToString());
            uint mDrop2 = uint.Parse(mobData[i]["drop2"].ToString());

            MOB_DATABASE.Add(new Mob(mID, mName, mType, mLevel, mHealth, mDivinity, mAtk1, mAtk2, mAtk3, mDrop1, mDrop2));
        }
    }

    private JsonData GetTextData(string filename) {
        return JsonMapper.ToObject(File.ReadAllText(Application.dataPath + fileDirectory + filename));
    }

    private void StoreDatabase() {
        foreach(Item item in ITEM_DATABASE) {
            ItemDB.Add(item.ID, item);
        }

        foreach (Weapon weapon in WEAPON_DATABASE) {
            WeaponDB.Add(weapon.ID, weapon);
        }

        foreach (Skill skill in SKILL_DATABASE) {
            SkillDB.Add(skill.ID, skill);
        }

        foreach (Mob mob in MOB_DATABASE) {
            MobDB.Add(mob.ID, mob);
        }

    }
}
