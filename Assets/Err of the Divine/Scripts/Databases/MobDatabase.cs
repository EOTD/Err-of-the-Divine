using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobDatabase : MonoBehaviour {

    public List<Mob> Database = new List<Mob>();

    public Dictionary<string, Mob> DB = new Dictionary<string, Mob>();

    private static MobDatabase instance;

    public static MobDatabase Instance {
        get { return instance; }

        set { instance = value; }
    }

    void Awake() {
        if(instance == null)
        instance = this;

        // ID, Name, Type, Level, Health, Divinity, Atk1, Atk2, Atk3, Drop1, Drop2
        Database.Add(new Mob(1000, "Mercury", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1001, "Feronia", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1002, "Mars", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        Database.Add(new Mob(1002, "Malice", MobType.Standard, 10, 1000, 100, 10, 20, 35, 1, 2));


        for (int i = 0; i < Database.Count; i++) {
            DB.Add(Database[i].Name, Database[i]);
        }
    }

}
