using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    [Header("Item Database")]
    [SerializeField] private List<Item> Database = new List<Item>();

    [Header("Weapon Data")]
    [SerializeField] private List<Weapon> WeaponDatabase = new List<Weapon>();

    public Dictionary<uint, Item> ItemDB = new Dictionary<uint, Item>();
    public Dictionary<uint, Weapon> WeaponDB = new Dictionary<uint, Weapon>();

    private static ItemDatabase instance;
    public static ItemDatabase Instance {
        get { return instance; }
        set { instance = value; }
    }

    void Awake() {
        if (instance == null)
            instance = this;

        BuildItemDatabase();
        BuildWeaponDatabase();
        StoreItemDatabase();

    }

    void BuildItemDatabase() {
        // ID - Name - Description - Type - Quality - Equipable - Stackable
        Database.Add(new Item(1000, "Jello", "A piece of Jello.", ItemType.Misc, ItemQuality.Common, false, true));

        Database.Add(new Item(2001, "Handgun","Just a basic gun.", ItemType.Weapon, ItemQuality.Common, true, false));
        Database.Add(new Item(2002, "Gladius", "A gun that's a little better than the Handgun.", ItemType.Weapon, ItemQuality.Rare, true, false));
        Database.Add(new Item(2003, "Cestus", "A strong and valuable gun.", ItemType.Weapon, ItemQuality.Epic, true, false));
        Database.Add(new Item(2004, "Hasta", "The strongest gun of them all.", ItemType.Weapon, ItemQuality.Legendary, true, false));
        Database.Add(new Item(2005, "Feronia's Whip", "The Goddess Feronia's sacred  weapon.", ItemType.Weapon, ItemQuality.Rare, true, false));
    }

    void BuildWeaponDatabase() {
        // ID - Type - Min - Max - Multiplier - Rate - Accuracy - ADS - Recoil - Range - Falloff Rate - Clip Size - Reload Speed
        WeaponDatabase.Add(new Weapon(2001, WeaponType.Range, 10, 25, 2.5f, 3.0f, 95, 50, 1.5f, 9, 7, 45, 2.5f));
        WeaponDatabase.Add(new Weapon(2002, WeaponType.Range, 10, 25, 2.5f, 3.0f, 95, 50, 1.5f, 9, 7, 45, 2.5f));
        WeaponDatabase.Add(new Weapon(2003, WeaponType.Range, 10, 25, 2.5f, 3.0f, 95, 50, 1.5f, 9, 7, 45, 2.5f));
        WeaponDatabase.Add(new Weapon(2004, WeaponType.Range, 10, 25, 2.5f, 3.0f, 95, 50, 1.5f, 9, 7, 45, 2.5f));
        WeaponDatabase.Add(new Weapon(2005, WeaponType.Melee, 10, 25, 2.5f, 3.0f, 95, 50, 1.5f, 9, 7, 45, 2.5f));
    }

    void StoreItemDatabase() {
        foreach(Item item in Database) {
            ItemDB.Add(item.ID, item);
        }

        foreach (Weapon weapon in WeaponDatabase) {
            WeaponDB.Add(weapon.ID, weapon);
        }
    }
}
