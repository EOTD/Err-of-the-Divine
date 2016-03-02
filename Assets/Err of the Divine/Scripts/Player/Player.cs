using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [Header("Player Statistics")]
    public Stats stat;
    private static Player instance;

    public float currentDivinity;

    public static Player Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    public enum Mode
    {
        Aggression,
        Standby, // Regeneration

    }

    void Awake() {

        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start () {
        //stat = new Stats(100f,1f,1f,1f,1f,1f,1f);
        stat = new Stats();

        currentDivinity = stat.playerDivinity;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
