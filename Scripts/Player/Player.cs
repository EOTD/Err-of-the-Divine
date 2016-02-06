using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [Header("Player Statistics")]
    public Stats stat;

    public enum Mode
    {
        Aggression,
        Standby, // Regeneration

    }

	// Use this for initialization
	void Start () {
        //stat = new Stats(100f,1f,1f,1f,1f,1f,1f);
        stat = new Stats();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
