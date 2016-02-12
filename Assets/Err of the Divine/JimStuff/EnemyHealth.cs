using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int scoreValue = 10;

	public int Health = 10;

	public int superFuel = 45;

	ScoreManager_Super superMng;

	// Use this for initialization
	void Start () {
		superMng = ScoreManager_Super.Instance;
	}
	
	// Update is called once per frame
	void Update () {

		if(Health <= 0)
		{
			Debug.Log("I'M DEAD");
			ScoreManager_Kill.killCount += 1;
			Destroy(this.gameObject);
			//ScoreManager_Super.superCharge += 1;
			if(superMng.superCharge < 100)
			{
				preventSuperExceed();
				superMng.superCharge += superFuel;
			}
			else if(superMng.superCharge >=100)
			{
				superFuel = 0;
			}

		}

	
	}

	void preventSuperExceed()
	{
		if(superMng.superCharge + superFuel <90)
		{
			//Proceed
			this.superFuel = superFuel;
		}
		else if (superMng.superCharge + superFuel >=90)
		{
			//Do Not proceed
			superFuel = (100 - superMng.superCharge);
		}

	}

	void OnCollisionEnter(Collision bullet)
	{
		if (bullet.gameObject.tag == "Bullet") 
		{
			Health -= 1;

			ScoreManager.score += scoreValue;
			ScoreManager.hit += 1.0f;
			Debug.Log("THIS SHIT IS HIT");
		}
	}
}
