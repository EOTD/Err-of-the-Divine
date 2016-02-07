using UnityEngine;
using System.Collections;

public enum AbilityType {
    Support, Aggressive, Ground
}

public interface AbilityInterface {
    uint ID { get; set; }
    string Name { get; set; }
    AbilityType Type { get; set; }
    uint Cost { get; set; }
    uint AoE { get; set; }
    uint Range { get; set; }
    float Cooldown { get; set; }

}
