using UnityEngine;
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
	
	
	public bool isDead, isCaught, isRun , isIdle;
	
	private IEnumerator FSM(){
		while (!isDead) {
            DeathCheck();
            Animations();
            switch (state){

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

        currentHealth = maxHealth;

        mercury = GameObject.FindGameObjectWithTag ("Mercury").GetComponent<MercuryAI> ();
        Controller = GetComponent<Animator>();

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

        isIdle = true;

		//Debug.Log ("Idling");
	}

    // Wander Variables
    private float wanderTime;
    public float wanderDelay = 3;
    private float maxWalkDistance = 20.0f;
    private float minWalkDistance = 10f;
    Vector3 destination;

    private void Wander() {
        agent.speed = 7;
        agent.stoppingDistance = 5;

        
        if (Time.time > wanderTime) {
            Vector3 direction = Random.insideUnitSphere * maxWalkDistance;
            direction += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(direction, out hit, Random.Range(minWalkDistance, maxWalkDistance), 1);

            destination = hit.position;
            wanderTime = Time.time + wanderDelay;
        } 

        if(Time.time < wanderTime) {
            agent.Resume();
            agent.SetDestination(destination);
            isRun = true;
            isIdle = false;
        }

        if(Vector3.Distance(transform.position,destination) <= agent.stoppingDistance) {
            agent.SetDestination(transform.position);
            agent.Stop();
            isRun = false;
            isIdle = true;
        }
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

    void ResetAnimations() {
        if (currentState != state) {
            currentState = state;
            isRun = false;
            isIdle = false;
        }
    }

    Animator Controller;
    void Animations() {
        ResetAnimations();

        if (isRun) {
            Controller.SetBool("Moving", true);
        }
        else
            Controller.SetBool("Moving", false);


        if (isIdle) {
            Controller.SetBool("Idle", true);
        }
        else
            Controller.SetBool("Idle", false);
    }

    void DeathCheck() {
        if(currentHealth <= 0) {
            isDead = true;
            agent.Stop();
            gameObject.SetActive(false);
        }
    }
	
}
