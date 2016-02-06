// Attach this script to all NPCs that we want to see through walls
// Make sure all NPCs are on layer 10

using UnityEngine;
using System.Collections;

public class ChangeMat : MonoBehaviour {

	public Material normal;
	public Material penetrate;

	public bool norm = true; // Are we on the normal mat or penetrate mat?
	
	public void changeMat(){
		norm = !norm;
		if(!norm){
			this.GetComponentInChildren<MeshRenderer>().material = penetrate;
		}
		else{
			this.GetComponentInChildren<MeshRenderer>().material = normal;
		}
	}
}
