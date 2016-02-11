using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Weapon : WeaponInterface {
    [SerializeField] uint weaponID;
    [SerializeField] WeaponType weaponType;
    [SerializeField] uint weaponMinDmg;
    [SerializeField] uint weaponMaxDmg;
    [SerializeField] float weaponMultiplier;
    [SerializeField] float weaponRate;
    [SerializeField] float weaponAccuracy;
    [SerializeField] float weaponADS;
    [SerializeField] float weaponRecoil;
    [SerializeField] uint weaponRange;
    [SerializeField] float weaponFallOff;
    [SerializeField] uint weaponClipSize;
    [SerializeField] float weaponReloadSpd;

    public Weapon(uint id, WeaponType type, uint min, uint max, float multiplier, float rate, float accuracy, float ads, float recoil, uint range, float falloff, uint size, float reloadSpd) {
        weaponID = id;
        weaponType = type;
        weaponMinDmg = min;
        weaponMaxDmg = max;
        weaponMultiplier = multiplier;
        weaponRate = rate;
        weaponAccuracy = accuracy;
        weaponADS = ads;
        weaponRecoil = recoil;
        weaponRange = range;
        weaponFallOff = falloff;
        weaponClipSize = size;
        weaponReloadSpd = reloadSpd;
    }

    public Weapon(uint id, uint type, uint min, uint max, float multiplier, float rate, float accuracy, float ads, float recoil, uint range, float falloff, uint size, float reloadSpd) {
        weaponID = id;
        weaponType = (WeaponType)type;
        weaponMinDmg = min;
        weaponMaxDmg = max;
        weaponMultiplier = multiplier;
        weaponRate = rate;
        weaponAccuracy = accuracy;
        weaponADS = ads;
        weaponRecoil = recoil;
        weaponRange = range;
        weaponFallOff = falloff;
        weaponClipSize = size;
        weaponReloadSpd = reloadSpd;
    }

    public uint ID {
        get { return weaponID; }
        set { weaponID = value; }
    }

    public WeaponType Type {
        get { return weaponType; }
        set { weaponType = value; }
    }

    public uint MinDamage {
        get { return weaponMinDmg; }
        set { weaponMinDmg = value; }
    }

    public uint MaxDamage {
        get { return weaponMaxDmg; }
        set { weaponMaxDmg = value; }
    }

    public float Multiplier {
        get { return weaponMultiplier; }
        set { weaponMultiplier = value; }
    }

    public float Rate {
        get { return weaponRate; }
        set { weaponRate = value; }
    }

    public float Accuracy {
        get { return weaponAccuracy; }
        set { weaponAccuracy = value; }
    }

    public float ADS {
        get { return weaponADS; }
        set { weaponADS = value; }
    }

    public float Recoil {
        get { return weaponRecoil; }
        set { weaponRecoil = value; }
    }

    public uint Range {
        get { return weaponRange; }
        set { weaponRange = value; }
    }

    public float FallOff {
        get { return weaponFallOff; }
        set { weaponFallOff = value; }
    }

    public uint ClipSize {
        get { return weaponClipSize; }
        set { weaponClipSize = value; }
    }

    public float ReloadSpd {
        get { return weaponReloadSpd; }
        set { weaponReloadSpd = value; }
    }
}
