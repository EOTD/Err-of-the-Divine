using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionAI : MonoBehaviour {


	/* ------------ TO DO ---------------
	 * Enemy Attacks from Range and closes gap within player during that time
	 * Enemy switching to melee once it's close to player.
	 * Enemy is STATIONARY and will NOT try to dodge attacks or anything.
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 * -----------------------------------/

	
	/* --------- HP / Hunger ---------*/
	//	public int currentHealth, maxHealth;
	
	/* --------- States ---------*/
	public enum State {
		Idle, Patrol, Wander, Flee, Stunned, Pursue
    }
	public State state; 
	private State currentState;
	private State defaultState = State.Wander;
	
	/* --------- Navmesh ---------*/
	private NavMeshAgent agent;
	private int stoppingDistance = 3;
	private float speed;
	private float walkSpeed = 7f;
	private float runSpeed = 10f;
	private int rotationSpeed = 120;
	
	/* --------- Waypoints ---------*/
	public Transform[] wayPoints;
	private int waypointIndex;

    //------------------------------//

    // Stunned Variables
    public float stunTime;
    public float stunDelay = 10;

    public bool isStunned, isPursue, isWalk, isIdle, isDead;

    private GameObject player;



    private IEnumerator FSM(){
		while (!isDead) {

            switch (state) {

                case State.Idle:
                    Idle();
                    break;

                case State.Patrol:
                    Patrol(); // Default State
                    break;

                case State.Wander:
                    Wander();
                    break;
                case State.Stunned:
                    Stunned();
                    break;
                case State.Pursue:
                    Pursue();
                    break;
            }
			
			yield return null;
			
		}
		
		yield return null;
	}

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {
		
		/* Nav Mesh Properties */
		agent.speed = walkSpeed; // Default is walk.
		agent.angularSpeed = rotationSpeed;
		
		////////////////////////////////////
		state = defaultState;
		
		StartCoroutine ("FSM");
		
	}

    private void Pursue() {
        isPursue = true;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 dir = (player.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);

        // Smooth Look-At Player
        if (direction < 1f) {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.5f * Time.deltaTime);
        }

        if (isPursue) {

            if (Utilities.InRange(transform,player.transform,stoppingDistance)) {
                //					Debug.Log ("isIdle");
                isWalk = false;
                isPursue = false;
                isIdle = true;
                //state = State.Attack;
            }
            else {
                //					Debug.Log ("isWalk");
                isIdle = false;
                isWalk = true;
                agent.Resume();
                agent.SetDestination(player.transform.position);

            }

        }
    }

    private void Patrol(){
		Debug.Log ("Patrolling");
		
		if (wayPoints [0] == null) {
			Debug.LogWarning ("Waypoint is not assigned");
			return;
		}
		
		if(Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < stoppingDistance){
			if(waypointIndex >= wayPoints.Length - 1) 
				waypointIndex = 0;
			else 
				waypointIndex++;
		}
		agent.SetDestination(wayPoints[waypointIndex].position);
	}
	
	private void Idle(){
		Debug.Log ("Idling");
	}
	
	private void Wander(){
		Debug.Log ("Wandering");
	}

    public void Stunned() {
        Debug.Log(gameObject.name + " is Stunned");
        agent.Stop();
        //isAttack = true;
        if (stunTime == 0) {
            isStunned = true;
            StartCoroutine(StunCooldown());
        }
    }

    private IEnumerator StunCooldown() {
        while (stunTime < stunDelay) {
            stunTime += Time.deltaTime;
            yield return null;
        }

        // Reset the cooldown when attack time has exceeded
        if (stunTime >= stunDelay) {
            stunTime = 0;
            //if(stunEndAnimation){ // 
            state = State.Pursue;
            isStunned = false;
            //}
        }

        yield return null;
    }

}
