using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager_Super : MonoBehaviour {

	public int superCharge = 0;

	//Ask Shih Ho for detail. 
	//Not certain why this method is necessary but it works for some reason.
	private static ScoreManager_Super instance;

	public static ScoreManager_Super Instance {
		get {
			return instance;
		}
		set {
			instance = value;
		}
	}

	public int currentCharge = 0;

	Text superIndicator;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		superIndicator = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		/*
		while(0 <= superCharge && superCharge < 100)
		{
			superIndicator.text = "Charge Meter " + superCharge;
		}
		*/

		Debug.Log(superCharge);
		/*
		if(superCharge <= 0  && superCharge < 100)
		{
			superIndicator.text = "Charge Meter " + superCharge;
		}
		else if(superCharge >= 100)
		{
			superIndicator.text = "FULLY CHARGED!!!";
		}
		*/

		if(superCharge >= 100)
		{
			superIndicator.text = "Fully Charged";
		}
		else
		{
			superIndicator.text = ""+superCharge+"";
		}
	}
}
