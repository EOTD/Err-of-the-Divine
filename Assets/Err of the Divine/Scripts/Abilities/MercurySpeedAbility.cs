using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson{

	public class MercurySpeedAbility : MonoBehaviour {

		private FirstPersonController controller;

		void Start () {
			controller = GetComponent<FirstPersonController>();
		}

		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.F)){
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
