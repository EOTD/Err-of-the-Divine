using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FP_Shooting : MonoBehaviour {

	public GameObject bullet_prefab;
	float bulletImpulse = 20f;

	//Object Pooling
	public float fireTime = 0.05f;
	public int pooledAmount = 20;
	List <GameObject> bullets;

	ScoreManager_Super supMeter;

	// Use this for initialization
	void Start () {

		//Object Pooling
		bullets = new List<GameObject>();
		for(int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(bullet_prefab);
			obj.SetActive(false);
			bullets.Add(obj);
		}

		supMeter = ScoreManager_Super.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetButtonDown("Fire1") ) {
			Camera cam = Camera.main;
			GameObject thebullet = (GameObject)Instantiate(bullet_prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
			thebullet.GetComponent<Rigidbody>().AddForce( cam.transform.forward * bulletImpulse, ForceMode.Impulse);

			ScoreManager.totalShot += 1.0f;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			supMeter.superCharge -= 25;
		}

	}


	//Object Pooling
	void Fire()
	{
		for(int i = 0;i<bullets.Count;i++)
		{
			if(!bullets[i].activeInHierarchy)
			{
				bullets[i].transform.position = transform.position;
				bullets[i].transform.rotation = transform.rotation;
				bullets[i].SetActive(true);
				break;
			}
		}
	}
	/*
	void OnCollisionEnter()
	{
		Debug.Log ("Hit Something");
	}
	*/
}
