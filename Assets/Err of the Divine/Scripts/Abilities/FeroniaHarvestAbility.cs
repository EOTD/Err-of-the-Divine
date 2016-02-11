// Attach this script to the player

// This script is for player input, the other FeroniaHarvest script is for the harvesting effects

using UnityEngine;
using System.Collections;

public class FeroniaHarvestAbility : MonoBehaviour {
	
	public float mainCost; // How much divinity is depleted when ability is used
	public float subCost;

	private bool main = true; // Are we on the main or sub ability?

	public float penetrateRadius = 500f;
	
	private AbilityManager manager; // Get the AbilityManager script to keep track of current ability

	[HideInInspector]
	public bool isHarvesting = false;
	[HideInInspector]
	public float harvestTime = 10000f; // For the duration of the harvest, used to know when the player is holding down F or continually pressing F

	// Temp variables until animation is ready
	public GameObject whip; // For testing I'm using a box collider
	
	void Start(){
		manager = GetComponent<AbilityManager>();
	}
	
	void Update () {
		// Begin check main ability
		if (Input.GetKeyDown (KeyCode.F) && manager.abilities [manager.i].ToString () == "Feronia" && !isHarvesting && main && manager.divinity > mainCost) {
			manager.divinity -= mainCost;
			// Play animation
			Debug.Log ("murrrrrrrr");
			whip.SetActive (true); // Temporary for testing
			isHarvesting = true;
		}

		else if (Input.GetKeyUp (KeyCode.F) && manager.abilities [manager.i].ToString () == "Feronia" && isHarvesting && main) {
			harvestTime = 0.5f;
			Debug.Log("Am I here?");
		}

		
		if (harvestTime > 0 && isHarvesting) {
			harvestTime -= Time.deltaTime;
		}
		else if (harvestTime <= 0 && isHarvesting){
			whip.SetActive(false); // Temporary for testing
			isHarvesting = false;
			harvestTime = 10000f;
		}
	// End main ability checks

	// Begin sub ability checks
		else if (Input.GetKeyDown (KeyCode.F) && manager.abilities [manager.i].ToString () == "Feronia" && !main) {
			manager.divinity -= subCost;

			Collider[] colliders = Physics.OverlapSphere (this.transform.position, penetrateRadius, 1 << 10); // Find all colliders on layer 10
			foreach (Collider hit in colliders) {

				ChangeMat mat = hit.GetComponent<ChangeMat> ();

				if (mat != null) {
					mat.changeMat (); // Call changeMat function to change the material to penetrate
				}
			}
		} else if (Input.GetKeyUp (KeyCode.F) && manager.abilities [manager.i].ToString () == "Feronia" && !main && manager.divinity > subCost) {

			Collider[] colliders = Physics.OverlapSphere (this.transform.position, penetrateRadius, 1 << 10);
			foreach (Collider hit in colliders) {

				ChangeMat mat = hit.GetComponent<ChangeMat> ();
				
				if (mat != null) {
					mat.changeMat (); // Change material back to the normal one
				}
			}
		}
	// End sub ability checks

	// Toggle between main and sub abilities
		if (Input.GetKeyDown (KeyCode.T)){
			Debug.Log("not main");
			main = !main;
		}
	}
}
