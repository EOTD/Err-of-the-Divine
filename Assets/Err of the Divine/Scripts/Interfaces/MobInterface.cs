using UnityEngine;
using System.Collections;


public enum MobType {
    Standard, Mediocre, Advanced, Mini_Boss, Boss
}

public interface MobInterface { // Interface Mob Database
    uint ID { get; set; }
    string Name { get; set; }
    MobType Type { get; set; }
    uint Level { get; set; }
    float Health { get; set; }
    uint Divinity { get; set; }
    uint Atk1 { get; set; }
    uint Atk2 { get; set; }
    uint Atk3 { get; set; }
    uint DeathDrop { get; set; }
    uint HarvestDrop { get; set; }

}
