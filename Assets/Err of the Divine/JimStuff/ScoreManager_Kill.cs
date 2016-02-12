using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager_Kill : MonoBehaviour {

	public static int killCount;
	
	Text killNumber;
	
	void Awake()
	{
		killNumber = GetComponent<Text>();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		killNumber.text = "Enemy Killed: " + killCount;
	}
}
