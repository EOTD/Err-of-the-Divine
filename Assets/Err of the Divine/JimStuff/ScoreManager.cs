using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score; //Testing placeholder

	public static float hit = 1.0f;
	public static float totalShot = 1.0f;
	public static float accuracy;

	Text scoreText;
	Text AccText;

	void Awake()
	{
		scoreText = GetComponent<Text> ();
		AccText = GetComponent<Text> ();

		score = 0;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		accuracy = hit/totalShot;
		scoreText.text = "Score: " + score;
		AccText.text = "Accuracy: " + accuracy*100 + "%";


	}
}
