﻿using UnityEngine;
using System.Collections;

public class CivillianAI : MonoBehaviour {

    Mob md; // Monster Data
	
	/* --------- HP / Hunger ---------*/
	[Header("Health")]
	public int currentHealth;
	public int maxHealth;
	
	/* --------- States ---------*/
	public enum State {
		Idle, Patrol, Wander, Flee, Captured
	}
	[Header("State Machine")]
	public State defaultState;
	public State state; 
	private State currentState;
	
	/* --------- Navmesh ---------*/
	private NavMeshAgent agent;
	private int stoppingDistance = 3;
	private float speed;
	[Header("Speed")]
	public float walkSpeed = 7f;
	public float runSpeed = 10f;
	private int rotationSpeed = 120;
	
	/* --------- Waypoints ---------*/
	[Header(" ")]
	public Transform[] wayPoints;
	private int waypointIndex;
	
	//------------------------------//

	/* --------- Interactions ---------*/
	private MercuryAI mercury;
	
	
	public bool isDead, isCaught;
	
	private IEnumerator FSM(){
		while (!isDead) {
			
			switch(state){

			case State.Idle:
				Idle();
				break;
				
			case State.Patrol:
				Patrol(); // Default State
				break;
				
			case State.Wander:
				Wander();
				break;
			case State.Flee:
				Flee();
				break;
			case State.Captured:
				Captured();
				break;
			}
			
			yield return null;
			
		}
		
		yield return null;
	}
	
	// Use this for initialization
	void Start () {
        // Getting the Monster Data here from the Mob Database
        // md = Utilities.GetMobData("Malice");
        /////////////////////////////////////////////////////

        mercury = GameObject.FindGameObjectWithTag ("Mercury").GetComponent<MercuryAI> ();

		/* Nav Mesh Properties */
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = walkSpeed; // Default is walk.
		agent.angularSpeed = rotationSpeed;
		
		////////////////////////////////////
		state = defaultState;
		
		StartCoroutine ("FSM");
		
	}
	
	private void Patrol(){
		agent.enabled = true;
		Debug.Log ("Patrolling");

		try { Debug.Log (wayPoints [0]); }
		catch {	Debug.LogWarning ("Waypoint is not assigned");  return;}

		if(Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < stoppingDistance){
			if(waypointIndex >= wayPoints.Length - 1) 
				waypointIndex = 0;
			else 
				waypointIndex++;
		}
		agent.SetDestination(wayPoints[waypointIndex].position);
	}
	
	private void Idle(){
		agent.enabled = true;
		//Debug.Log ("Idling");
	}
	
	private void Wander(){
		agent.enabled = true;
		Debug.Log ("Wandering");
	}

	private void Flee(){
		agent.enabled = true;

	}

	private void Captured(){
		Debug.Log ("Captured");
		agent.enabled = false;
		transform.position = mercury.capturePoint.position;
		transform.localRotation = mercury.capturePoint.localRotation;
		transform.SetParent(GameObject.FindGameObjectWithTag("Mercury").transform);
	}

	private void OnTriggerEnter(Collider col){
		switch (col.gameObject.tag) {

		case "Mercury":
			state = State.Flee;
			break;
		}

	}
	
}
