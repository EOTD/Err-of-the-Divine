using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManger_Timer : MonoBehaviour {

	float timeCounting = 0.0f;
	float timeRemaing = 99.9f;

	Text timer;

	void Awake()
	{
		timer = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaing -= Time.deltaTime;
		timeCounting += Time.deltaTime;

		timer.text = "Time Remaining: " + timeCounting;
	}
}
