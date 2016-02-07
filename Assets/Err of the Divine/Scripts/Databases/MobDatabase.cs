using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobDatabase : MonoBehaviour {

    [SerializeField] private List<Mob> Database = new List<Mob>();

    public Dictionary<string, Mob> DB = new Dictionary<string, Mob>();

    private static MobDatabase instance;

    public static MobDatabase Instance {
        get { return instance; }
        set { instance = value; }
    }

    void Awake() {
        if(instance == null)
        instance = this;

        BuildDatabase();
        StoreDatabase();
    }

    // Creates a Mob with specified information and then adds them into the Database list.
    void BuildDatabase() {
                           // ID, Name, Type, Level, Health, Divinity, Atk1, Atk2, Atk3, Drop1, Drop2
        Database.Add(new Mob(1000, "Mercury", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1001, "Feronia", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1003, "Mars", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1004, "Malice", MobType.Standard, 10, 1000, 100, 10, 20, 35, 1, 2));
    }

    // Takes all the Mobs in the Database and stores them into a Dictionary with
    // the specified key. We use this to get the stored information faster.
    void StoreDatabase() {
        for (int i = 0; i < Database.Count; i++) {
            DB.Add(Database[i].Name, Database[i]);
        }
    }

}
