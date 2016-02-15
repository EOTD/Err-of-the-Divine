using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel(string name)
	{
		Debug.Log("Level Load reqquested for: " +name);
		Application.LoadLevel(name);
	}
	
	public void QuitRequest()
	{
		Debug.Log("I want to quit");
		Application.Quit();
	}
}
