// Attach this script to player

using UnityEngine;
using System.Collections;
using System.Collections.Generic; // This is needed for using lists

public class AbilityManager : MonoBehaviour {

	// The name of the abilities are the same as the god they get the ability from
	[HideInInspector]
	public enum Ability{Feronia, Jupiter, Mercury};

	[HideInInspector]
	public List<Ability> abilities = new List<Ability>(); // Using a list because it's easier to add to a list than to an array

	[HideInInspector]
	public float divinity = 100f;

	//[HideInInspector]
	public int i = 0;

	// Adding things to the list of abilities for testing purposes
	// In the game these will be added as the player harvests the souls of gods
	void Awake(){
		abilities.Clear();
		abilities.Add(Ability.Feronia);
		abilities.Add(Ability.Jupiter);
		abilities.Add(Ability.Mercury);
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.E)){
			// Cycle through the list of abilities using the left shift key, can be changed if needed
			if(i < abilities.Count-1){
				i++;
			}
			else{
				i = 0;
			}
			Debug.Log(abilities[i].ToString());
		}
	}
}
