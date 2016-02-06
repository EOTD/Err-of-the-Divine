// Attach to whip object
// This script handles what happens when the whip hits an enemy

using UnityEngine;
using System.Collections;

public class FeroniaHarvest : MonoBehaviour {

	public bool enemyHit = false;
	private MercuryAI mAI;

	// Variables that affect the enemy
	public int damage;
	public FeroniaHarvestAbility harvest;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag != "Player"){ // because we don't want the grenade to disappear as soon as you throw it

			mAI = other.GetComponent<MercuryAI>();
			
			if (mAI != null){
				Debug.Log("Hit Enemy");
				enemyHit = true;
				mAI.state = MercuryAI.State.Idle;
				//hit.GetComponent<MercuryAI>().state = MercuryAI.State.Idle;
				
				if (enemyHit == true){
					if (Input.GetKeyDown(KeyCode.F) && harvest.isHarvesting){
						mAI.currentHealth -= damage;
					}
				}
			}
		}
	}
}
