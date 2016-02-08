using UnityEngine;
using System.Collections;

public enum WeaponType {
    Melee, Range
}

public interface WeaponInterface {

    uint ID { get; set; }
    WeaponType Type { get; set; }
    uint MinDamage { get; set; }
    uint MaxDamage { get; set; }
    float Multiplier { get; set; }
    float Rate { get; set; }
    float Accuracy { get; set; }
    float ADS { get; set; }
    float Recoil { get; set; }
    uint Range { get; set; } // Fall off Range
    float FallOff { get; set; }
    uint ClipSize { get; set; }
    float ReloadSpd { get; set; }

}
