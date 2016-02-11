using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Skill : AbilityInterface {

    [SerializeField] private uint skillID;
    [SerializeField] private string skillName;
    [SerializeField] private AbilityType skillAbility;
    [SerializeField] private SkillType skillType;
    [SerializeField] private uint skillCost;
    [SerializeField] private uint skillAoe;
    [SerializeField] private uint skillRange;
    [SerializeField] private float skillCooldown;

    public Skill(uint id,  string name, AbilityType ability, SkillType type, uint cost, uint aoe, uint range, float cooldown) {
        skillID = id;
        skillName = name;
        skillAbility = ability;
        skillType = type;
        skillCost = cost;
        skillAoe = aoe;
        skillRange = range;
        skillCooldown = cooldown;
    }

    public Skill(uint id, string name, uint ability, uint type, uint cost, uint aoe, uint range, float cooldown) {
        skillID = id;
        skillName = name;
        skillAbility = (AbilityType)ability;
        skillType = (SkillType)type;
        skillCost = cost;
        skillAoe = aoe;
        skillRange = range;
        skillCooldown = cooldown;
    }

    public Skill() {
        skillID = 0;
    }

    public uint ID { get { return skillID; } set { skillID = value; } }
    public string Name { get { return skillName; } set { skillName = value; } }
    public AbilityType Ability { get { return skillAbility; } set { skillAbility = value; } }
    public SkillType Type { get { return skillType; } set { skillType = value; } }
    public uint Cost { get { return skillCost; } set { skillCost  = value; } }
    public uint AoE { get { return skillAoe; } set { skillAoe = value; } }
    public uint Range { get { return skillRange; } set { skillRange = value; } }
    public float Cooldown { get { return skillCooldown; } set { skillCooldown = value; } }
}
