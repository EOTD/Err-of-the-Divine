using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager_Harvest : MonoBehaviour {

	public static int soulHarvest;

	Text Harvested;

	void Awake()
	{
		Harvested = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Harvested.text = "Harvested: " + soulHarvest;
	}
}
