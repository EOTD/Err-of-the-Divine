using UnityEngine;
using System.Collections;

public class SoulBehavior : MonoBehaviour {

	public int soulCount = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider soul)
	{
		if (soul.gameObject.tag == "Player") 
		{
			/*
			ScoreManager.score += scoreValue;
			ScoreManager.hit += 1.0f;
			Debug.Log("THIS SHIT IS HIT");
			*/

			ScoreManager_Harvest.soulHarvest += soulCount;
			Destroy(this.gameObject);
		}
	}
}
