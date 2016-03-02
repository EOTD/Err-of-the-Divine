using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson{

	public class MercurySpeedAbility : MonoBehaviour {

		private FirstPersonController controller;

		private float castCost; // How much divinity is depleted when this ability is used

		private AbilityManager manager; // Get the AbilityManager script to keep track of current ability

		void Start(){
			manager = GetComponent<AbilityManager>();
			controller = GetComponent<FirstPersonController>();
		}

		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.F) && manager.abilities[manager.i].ToString() == "Mercury" && manager.divinity > castCost){
				manager.divinity -= castCost;
				// Yuima goes fast
				controller.m_RunSpeed = 50f;
				controller.m_WalkSpeed = 50f;
				Invoke("revertSpeed", 0.3f);
			}
		}

		void revertSpeed(){
			controller.m_RunSpeed = 10f;
			controller.m_WalkSpeed = 5f;
		}
	}
}
