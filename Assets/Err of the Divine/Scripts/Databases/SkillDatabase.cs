using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDatabase : MonoBehaviour {

    private static SkillDatabase instance;

    public List<Skill> Database = new List<Skill>();

    public Dictionary<string, Skill> DB = new Dictionary<string, Skill>();

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

    void Start() {
        Skill sd = Utilities.GetSkillData("Feronia");

        Debug.Log(sd.ID);
    }

    private void BuildDatabase() {
        Database.Add(new Skill(4000, "Feronia", AbilityType.Support, 10, 0, 0, 1000));

    }
    private void StoreDatabase() {
        foreach (Skill skill in Database) {
            DB.Add(skill.Name, skill);
        }

    }
}
