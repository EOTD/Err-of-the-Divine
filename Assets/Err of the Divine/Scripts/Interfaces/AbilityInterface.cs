using UnityEngine;
using System.Collections;

public enum SkillType {
    Support, Aggressive, Ground
}

public enum AbilityType {
    Feronia, Jupiter, Mars, Mercury, Venus, Vulcan, Divine
}

public interface AbilityInterface {
    uint ID { get; set; }
    string Name { get; set; }
    SkillType Type { get; set; }
    AbilityType Ability { get; set; }
    uint Cost { get; set; }
    uint AoE { get; set; }
    uint Range { get; set; }
    float Cooldown { get; set; }

}
