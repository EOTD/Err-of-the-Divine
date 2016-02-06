// Attach script to player
// The grenade game object must have a rigidbody

// This script is for player input, the other JupiterBolt script is for the effect the ability has when it hits and enemy

using UnityEngine;
using System.Collections;

public class JupiterBoltAbility : MonoBehaviour {

	public GameObject grenade; // Object that is being thrown
	private GameObject grenadeClone;

	public float throwSpeed;
	
	private float castCost; // How much divinity is depleted when this ability is used

	private AbilityManager manager; // Get the AbilityManager script to keep track of current ability

	void Start(){
		manager = GetComponent<AbilityManager>();
	}

	void Update () { // Currently using left click to throw, can be changed if needed
		// Check for input and current ability
		// Check if player has enough divinity to cast
		if (Input.GetKeyUp(KeyCode.F) && manager.abilities[manager.i].ToString() == "Jupiter" && manager.divinity > castCost){
			manager.divinity -= castCost;
			// Play animation, add bool or ienum for timing throw with animation
			grenadeClone = Instantiate(grenade ,transform.position + transform.forward, new Quaternion(0,0,90,0)) as GameObject;
			grenadeClone.GetComponent<Rigidbody>().velocity = (transform.forward * throwSpeed) + transform.up;
		}
	}
}
