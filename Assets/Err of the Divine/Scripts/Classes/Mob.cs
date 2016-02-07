using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Mob : MobInterface {

    [SerializeField] private uint mobID;
    [SerializeField] private string mobName;
    [SerializeField] private MobType mobType;
    [SerializeField] private uint mobLevel;
    [SerializeField] private float mobHealth;
    [SerializeField] private uint mobDivinity;
    [SerializeField] private uint mobAtk1;
    [SerializeField] private uint mobAtk2;
    [SerializeField] private uint mobAtk3;
    [SerializeField] private uint mobDrop1;
    [SerializeField] private uint mobDrop2;

    public Mob(uint id, string name, MobType type, uint level, uint health, uint divinity, uint atk1, uint atk2, uint atk3, uint drop1, uint drop2) {
        mobID = id;
        mobName = name;
        mobType = type;
        mobLevel = level;
        mobHealth = health;
        mobDivinity = divinity;
        mobAtk1 = atk1;
        mobAtk2 = atk2;
        mobAtk3 = atk3;
        mobDrop1 = drop1;
        mobDrop2 = drop2;
    }

    public uint ID { get { return mobID; } set { mobID = value; } }
    public string Name { get { return mobName; } set { mobName = value; } }
    public MobType Type { get { return mobType; } set { mobType = value; } }
    public uint Level { get { return mobLevel; } set { mobLevel = value; } }
    public float Health { get { return mobHealth; } set { mobHealth = value; } }
    public uint Divinity { get { return mobDivinity; } set { mobDivinity = value; } }
    public uint Atk1 { get { return mobAtk1; } set { mobAtk1 = value; } }
    public uint Atk2 { get { return mobAtk2; } set { mobAtk2 = value; } }
    public uint Atk3 { get { return mobAtk3; } set { mobAtk3 = value; } }
    public uint DeathDrop { get { return mobDrop1; } set { mobDrop1 = value; } }
    public uint HarvestDrop { get { return mobDrop2; } set { mobDrop2 = value; } }

}
