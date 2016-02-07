using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Skill : AbilityInterface {

    [SerializeField] private uint skillID;
    [SerializeField] private string skillName;
    [SerializeField] private AbilityType skillType;
    [SerializeField] private uint skillCost;
    [SerializeField] private uint skillAoE;
    [SerializeField] private uint skillRange;
    [SerializeField] private float skillCooldown;

    public Skill(uint id,  string name, AbilityType type, uint cost, uint aoe, uint range, float cooldown) {
        skillID = id;
        skillName = name;
        skillType = type;
        skillCost = cost;
        skillAoE = aoe;
        skillRange = range;
        skillCooldown = cooldown;
    }

    public uint ID { get { return skillID; } set { skillID = value; } }
    public string Name { get { return skillName; } set { skillName = value; } }
    public AbilityType Type { get { return skillType; } set { skillType = value; } }
    public uint Cost { get { return skillCost; } set { skillCost  = value; } }
    public uint AoE { get { return skillAoE; } set { skillAoE = value; } }
    public uint Range { get { return skillRange; } set { skillRange = value; } }
    public float Cooldown { get { return skillCooldown; } set { skillCooldown = value; } }
}
