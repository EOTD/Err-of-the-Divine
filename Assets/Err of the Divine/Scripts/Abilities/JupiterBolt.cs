// Attach to the grenade object
// Things that the grenade hits must have rigidbodies if we use AddExplosionForce

using UnityEngine;
using System.Collections;

public class JupiterBolt : MonoBehaviour {

	// Variables that don't call other things
	public float blastRadius;
	public float explosionForce;
	public float delay; // If we don't want the grenade to disappear immediately

	// Variables that affect the enemy
	public int damage;
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "MainCamera"){ // because we don't want the grenade to disappear as soon as you throw it
			GrenadeEffect();
			Destroy(this.gameObject);
		}
	}

	// What happens when the grenade collides with something
	void GrenadeEffect(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
		foreach (Collider hit in colliders) {
//			Rigidbody rb = hit.GetComponent<Rigidbody>();
//			
//			if (rb != null)
//				rb.AddExplosionForce(explosionForce, transform.position, blastRadius, 3.0F);
			MercuryAI mAI = hit.GetComponent<MercuryAI>();

			if (mAI != null){
				Debug.Log("Hit Enemy");
				Utilities.Stun(mAI.gameObject);
				mAI.currentHealth -= damage;
				//hit.GetComponent<MercuryAI>().state = MercuryAI.State.Idle;
			}
		}
	}
}
