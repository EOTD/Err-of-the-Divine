using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDatabase : MonoBehaviour {

    private static SkillDatabase instance;

    [SerializeField] private List<Skill> Database = new List<Skill>();

    public Dictionary<uint, Skill> DB = new Dictionary<uint, Skill>();

    public static SkillDatabase Instance {
        get { return instance; }
        set { instance = value; }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }

        BuildDatabase();
        StoreDatabase();

    }
    // ID, Name, God, Type, Cost, AoE, Range, Cooldown
    // Type - 0=Support, 1=Aggressive, 2=Ground
    private void BuildDatabase() {
        Database.Add(new Skill(4001, "Harvest Whip",AbilityType.Feronia,0, 10, 0, 0, 1000));
        Database.Add(new Skill(4002, "Penetrate",AbilityType.Feronia,0, 10, 0, 0, 1000));
        Database.Add(new Skill(4003, "Absolute Harvest",AbilityType.Feronia,1, 10, 0, 0, 1000));

        Database.Add(new Skill(4004, "Thunderbolt", AbilityType.Jupiter, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4005, "Crackling Fist", AbilityType.Jupiter, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4006, "Violent Maelstrom", AbilityType.Jupiter, 1, 10, 9, 9, 3000));

        Database.Add(new Skill(4007, "Bastion", AbilityType.Mars, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4008, "Fortify", AbilityType.Mars, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4009, "Aspect of Radiance", AbilityType.Mars, 1, 10, 9, 9, 3000));

        Database.Add(new Skill(4010, "Rush", AbilityType.Mercury, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4011, "Flash", AbilityType.Mercury, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4012, "Maximum Velocity", AbilityType.Mercury, 1, 10, 9, 9, 3000));

        Database.Add(new Skill(4013, "Fade", AbilityType.Venus, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4014, "Seduction", AbilityType.Venus, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4015, "Blossom", AbilityType.Venus, 1, 10, 9, 9, 3000));

        Database.Add(new Skill(4016, "Channel", AbilityType.Vulcan, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4017, "Rise", AbilityType.Vulcan, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4018, "Vulcan's Fury", AbilityType.Vulcan, 1, 10, 9, 9, 3000));

        Database.Add(new Skill(4019, "Goddess Arsenal", AbilityType.Divine, 1, 10, 9, 9, 3000));
        Database.Add(new Skill(4020, "Divine Whip", AbilityType.Divine, 1, 10, 9, 9, 3000));

    }
    private void StoreDatabase() {
        foreach (Skill skill in Database) {
            DB.Add(skill.ID, skill);
        }

    }
}
