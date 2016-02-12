using UnityEngine;
using System.Collections;

public class killBullet : MonoBehaviour {

	public float duration = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if(duration<=0)
		{
			Destroy(this.gameObject);
		}
	}
}
