// Attach this to the player

// This script is for player input, the other MarsShield script is for the effect of the shield

using UnityEngine;
using System.Collections;

public class MarsShieldAbility : MonoBehaviour {

	public GameObject shield;

	private float castCost; // How much divinity is depleted when this ability is used
	private float continueCost; // Amount of divinity needed to keep shield up

	private AbilityManager manager; // Get the AbilityManager script to keep track of current ability
	
	void Start () {
		shield.SetActive(false);
		manager = GetComponent<AbilityManager>();
	}

	void Update () {
		// Check for input and current ability
		if (Input.GetKeyDown(KeyCode.F) && manager.abilities[manager.i].ToString() == "Mars" && manager.divinity > castCost){
			manager.divinity -= castCost;
			shield.SetActive(true);
			StartCoroutine("maintainShield");
		}

	}

	IEnumerator maintainShield(){
		if (manager.divinity > continueCost){
		// Check if player has enough charge to continue casting shield
		// if Yes
		yield return new WaitForSeconds(0.5f); // Get from charge and subtract the cost to maintain shield
			manager.divinity -= continueCost;
		}
		else {
			shield.SetActive(false);
		}

		if (Input.GetKeyUp(KeyCode.F)){
			shield.SetActive(false);
			yield break;
		}
		
		StartCoroutine("maintainShield");
	}
}
