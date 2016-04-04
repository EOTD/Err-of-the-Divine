using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson{

	public class MercurySpeedAbility : MonoBehaviour {

		private FirstPersonController controller;

		private float castCost; // How much divinity is depleted when this ability is used
		private float blinkCost;

		private float normalDash = 30f;
		private float blink = 50f;

		private AbilityManager manager; // Get the AbilityManager script to keep track of current ability

		private bool lookingScope = false;

        Player player;

		void Start(){
			manager = GetComponent<AbilityManager>();
			controller = GetComponent<FirstPersonController>();
            player = Player.Instance;
		}

		// Update is called once per frame
		void Update () {
			float move;
			move = Input.GetAxis("Vertical");
			if (!lookingScope){
				if (Input.GetKeyDown(KeyCode.F) && manager.abilities[manager.i].ToString() == "Mercury" && manager.divinity > castCost){
					if (move != 0){
						controller.notDashing = false;
						manager.divinity -= castCost;
						// Yuima goes fast
						controller.m_RunSpeed = normalDash;
						controller.m_WalkSpeed = normalDash;
						Invoke("revertSpeed", 0.3f);
		                player.currentDivinity -= 10f;
		                Debug.Log(player.currentDivinity);
					}
					else {
						Debug.Log("not moving");
						controller.defaultCharControl = false;
						controller.notDashing = false;
						controller.m_RunSpeed = normalDash;
						controller.m_WalkSpeed = normalDash;
						Invoke("revertSpeed", 0.3f);
					}
				}
			}
			else{
				if (Input.GetKeyDown(KeyCode.F) && manager.abilities[manager.i].ToString() == "Mercury" && manager.divinity > blinkCost){
					Debug.Log("not moving");
					controller.defaultCharControl = false;
					controller.notDashing = false;
					controller.m_RunSpeed = blink;
					controller.m_WalkSpeed = blink;
					Invoke("revertSpeed", 0.3f);
				}
			}
		}

		void revertSpeed(){
			controller.m_RunSpeed = 10f;
			controller.m_WalkSpeed = 5f;
			controller.notDashing = true;
			controller.defaultCharControl = true;
		}
	}
}
