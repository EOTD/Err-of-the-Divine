using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobDatabase : MonoBehaviour {

    public List<Mob> database = new List<Mob>();

    private static MobDatabase instance;

    public static MobDatabase Instance {
        get { return instance; }

        set { instance = value; }
    }

    void Awake() {
        if(instance == null)
        instance = this;
    }

    void Start () {
        // ID, Name, Type, Level, Health, Divinity, Min, Max
        database.Add(new Mob(1000, "Mercury", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1 ,2));
        database.Add(new Mob(1001, "Feronia", MobType.Boss,10, 10000, 150, 100, 150, 200, 1, 2));
        database.Add(new Mob(1002, "Mars", MobType.Boss, 10, 10000, 150, 100, 150, 200, 1, 2));
        database.Add(new Mob(1002, "Malice", MobType.Standard, 10, 1000, 100, 10, 20, 35, 1, 2));
    }

}
