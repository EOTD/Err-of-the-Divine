using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Item : ItemInterface {

    [SerializeField] uint itemID;
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;
    [SerializeField] ItemType itemType;
    [SerializeField] ItemQuality itemQuality;
    [SerializeField] bool itemEquipable;
    [SerializeField] bool itemStack;

    public Item(uint id, string name, string desc, ItemType type, ItemQuality quality, bool equipable,bool stackable) {
        itemID = id;
        itemName = name;
        itemDesc = desc;
        itemType = type;
        itemQuality = quality;
        itemEquipable = equipable;
        itemStack = stackable;
    }

    public uint ID {
        get { return itemID; }
        set { itemID = value; }
    }

    public string Name {
        get { return itemName; }
        set { itemName = value; }
    }

    public string Desc {
        get { return itemDesc; }
        set { itemDesc = value; }
    }
    public ItemType Type {
        get { return itemType; }
        set { itemType = value; }
    }

    public ItemQuality Quality {
        get { return itemQuality; }
        set { itemQuality = value; }
    }

    public bool Equipable {
        get { return itemEquipable; }
        set { itemEquipable = value; }
    }

    public bool Stackable {
        get { return itemStack; }
        set { itemStack = value; }
    }
}
