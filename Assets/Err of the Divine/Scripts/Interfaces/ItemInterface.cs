using UnityEngine;
using System.Collections;

public enum ItemType {
    Consumable, Weapon, Misc
}

public enum ItemQuality {
    Common, Rare, Epic, Legendary
    }

public interface ItemInterface {

    uint ID { get; set; }
    string Name { get; set; }
    ItemType Type { get; set; }
    ItemQuality Quality { get; set; }
    bool Stackable { get; set; }
    bool Equipable { get; set; }
    string Desc { get; set; }
}
