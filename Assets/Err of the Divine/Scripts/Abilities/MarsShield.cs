// Attach this to the shield object
// Shield object should have a trigger collider
// Shield obejct should be parented under player

using UnityEngine;
using System.Collections;

public class MarsShield : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		//if (other.gameObject.tag == "enemy or attack or bullet"){ // Change string to whatever tag the object that hurts the player has
			//other.gameObject.GetComponent<Rigidbody>().velocity = other.gameObject.GetComponent<Rigidbody>().velocity * -1; // Deflects a projectile...wouldn't work for non-projectiles
			//Destroy(other.gameObject); // Protects the player
		//}
	}
}
