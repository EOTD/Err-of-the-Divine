using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MercuryAI : MonoBehaviour {
	
	/* --------- Instance ---------*/
	private static MercuryAI instance;
	public static MercuryAI Instance {
		get { return instance; }
		set { instance = value; }
	}

    /* --------- Animator ---------*/
    private Animator Controller;

    /* --------- HP / Hunger ---------*/
    [Header("Statistics")]
	public int currentHealth; 
	public int maxHealth, currentHunger, maxHunger;

	/* --------- States ---------*/
	public enum State {
		Idle, Flee, Pursue, Capture, Patrol, Consume, Dodge, Attack, Stunned
	}
	[Header("State Machine")]
	public State defaultState;
	public State state; 
	private State currentState;

	/* --------- Behaviors ---------*/
	public enum Behavior {
		One, Two, Three, Four
	}
	public Behavior behavior;

	/* -------- Attack Types -------*/
	public enum AttackType {
		Melee, Range, Power
	}
	public AttackType attackType;

	/* --------- Navmesh ---------*/
	private NavMeshAgent agent;
	private int stoppingDistance = 3;

	[Header("Speed")]
	private float speed;
	public float walkSpeed = 10f;
	public float runSpeed = 15f;
	private int rotationSpeed = 120;
	
	/* --------- Waypoints ---------*/
	[Header(" ")]
	public Transform[] wayPoints;
	private int waypointIndex;

	/* --------- Interaction ---------*/
	[HideInInspector]
	public Transform myTransform;
	[Header("Interactions")]
	public Transform capturePoint;
	public GameObject target;
	private GameObject player;

    public GameObject bulletPrefab;
    public Transform attackPosition;

    [Header("Conditions")]
	public bool isDead;
	public bool isHungry, isDodging, isStunned, isRun, isIdle;

    [Header("Reactions")]
    public float stunTime;
    public float stunDelay = 10;

    private IEnumerator FSM(){
		while (!isDead) {
            Animations();
			CheckHunger(); // Checks and Toggle's current Hunger
			CheckHealth (); // Check and Toggle's current Health
			UpdateAIBehavior();

            switch (state) {

                case State.Idle:
                    Idle();
                    break;

                case State.Patrol:
                    Patrol();
                    break;

                case State.Pursue:
                    Pursue();
                    break;

                case State.Capture:
                    Capture();
                    break;

                case State.Stunned:
                    Stunned();
                    break;

                case State.Consume:
                    Consume();
                    break;
                case State.Dodge:
                    Dodge();
                    break;
                case State.Flee:
                    Flee();
                    break;

                case State.Attack:
                    Attack(attackType);
                    break;

            }

			yield return null;

		}

		yield return null;
	}

	/* --------- Dodging ---------*/
	public float dodgeTime;
	private float dodgeDelay = 0.3f;
	Vector3 dodgeDir = new Vector3(0f,0f,0f);
	Vector3 dodgePos = new Vector3 (0f, 0f, 0f);

	private int dodgeLimit;
	private int dodgeCount;
	public enum DodgeType {
		Randomize, Aggression, Evasive
	}
	private DodgeType dodgeType;

	private void Dodge(){
		dodgeTime += Time.deltaTime;

		if (dodgeCount < dodgeLimit) {
			// Goes in once to get the random direction ONCE until Dodging is false again.
			if (!isDodging) {

				switch(dodgeType){
				case DodgeType.Randomize:
					dodgeDir = directions [Random.Range (0, directions.Length)];
					break;
				case DodgeType.Aggression:
					dodgeDir = player.transform.position - transform.position + new Vector3(0,0,3);
					break;
				case DodgeType.Evasive:
					dodgeDir = transform.position - player.transform.position;
					break;
				}

				dodgePos = transform.position + dodgeDir;
				isDodging = true;
			}
		
		
			if (dodgeTime < dodgeDelay) {
				transform.position = Vector3.Lerp (transform.position, dodgePos, 10f * Time.deltaTime);
			} else {
				dodgeTime = 0f;
				isDodging = false;
				dodgeCount++;
			}
		}

		if (dodgeCount >= dodgeLimit) {
			ResetDodge();
			//agent.Stop();
			state = State.Pursue;
		}
	}

	private void ResetDodge(){
		dodgeCount = 0;
		dodgeLimit = 0;
	}

	private static Vector3[] directions = {
		//new Vector3(0f,0f,5f), // Forward
		new Vector3(3f,0f,3f), // Forward Right
		new Vector3(-3f,0f,3f), // Forward Left
		//new Vector3(0f,0f,-5f), // Back
		new Vector3(3f,0f,-3f), // Back Right
		new Vector3(-3f,0f,-3f), // Back Left
		new Vector3(-3f,0f,0f), // Left
		new Vector3(3f,0f,0f) // Right
	};

	/*--------------------------------*/

	// Use this for initialization
	void Start () {
		Initialize ();
		StartCoroutine ("FSM");
	}

	private void Idle(){
		Debug.Log ("Idling");
	}

	private void Patrol(){
		Debug.Log ("Patrolling");
		agent.Resume ();

		if(Vector3.Distance(transform.position, wayPoints[waypointIndex].position) < stoppingDistance){
			if(waypointIndex >= wayPoints.Length - 1) 
				waypointIndex = 0;
			else 
				waypointIndex++;
		}
		agent.SetDestination(wayPoints[waypointIndex].position);
	}

	// Attack 1 = Range
	// Attack 2 = Melee
	// Attack 3 = Mechanic: Whirlwind

	private float meleeRange = 8f;
	private float detectionRange = 15f;

	private void CheckPlayerDistance(int range){
		if (Vector3.Distance (transform.position, player.transform.position) > range) {

		}

	}

	/* ------ Attack Variables ------*/
	private bool isAttacking;

	public float shootTime, shootDelay;
	public float meleeTime, meleeDelay;
	private bool isShooting, isMelee;
	
	private IEnumerator ShootCooldown(){
		while (shootTime > 0) {
			shootTime -= Time.deltaTime;
			yield return null;
		} 
		shootTime = 0;
		isShooting = false;
		
		yield return null;
	}
	
	private IEnumerator MeleeCooldown(){
		while (meleeTime > 0) {
			meleeTime -= Time.deltaTime;
			yield return null;
		} 
		meleeTime = 0;
		isMelee = false;
		
		yield return null;
	}

	private void Attack(AttackType type){

		agent.Stop();
		if (!isAttacking) {
			isAttacking = true;
			StartCoroutine (InitiateAttack (type));
			//Debug.Log ("In Attack");
		}

		//float distance = Vector3.Distance (transform.position, player.transform.position);
		Vector3 dir = (player.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
		// Smooth Look-At Player
		if(direction < 1f){
			Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
		}
	}
	
	private IEnumerator InitiateAttack(AttackType type){
		switch (type) {
		case AttackType.Range:

			if(shootTime <= 0){
				isShooting = true;
				shootTime = shootDelay;
				Debug.Log ("Shooting");
                    //Utilities.Attack(player, -1f);
                    //Instantiate(bulletPrefab, attackPosition.position, attackPosition.rotation);
				StartCoroutine(ShootCooldown());
			} 
			
			InitiateAttackDodge(2);
			StartCoroutine (SecondaryAttack(type));
			yield return null;
			break;


		case AttackType.Melee:
			InitiateAttackDodge(1);

			while (dodgeCount < dodgeLimit) {
				AttackDodge(DodgeType.Aggression);
				yield return null;
			}

			StartCoroutine(SecondaryAttack(type));
			yield return null;
			break;
		}
	}

	private IEnumerator SecondaryAttack(AttackType type){
		switch (type) {
		case AttackType.Range:
			while (dodgeCount < dodgeLimit) {
				AttackDodge(DodgeType.Randomize);
				yield return null;
			}

			state = State.Pursue;
			isAttacking = false;
			yield return null;
			break;
		case AttackType.Melee:
			//Debug.Log ("Meleeing");
			
			if (meleeTime <= 0) {
				isMelee = true;
				meleeTime = meleeDelay;
                Utilities.AdjustHealth(player, -5);
				Debug.Log ("Melee Attack!");
				StartCoroutine (MeleeCooldown ());

			} 

			StartCoroutine(AvoidPlayer());

			yield return null;
			break;
		}
	}

	private IEnumerator AvoidPlayer(){
		InitiateAttackDodge(2);
		while (dodgeCount < dodgeLimit) {
			AttackDodge (DodgeType.Evasive);
			yield return null;
		}

		isAttacking = false;
		state = State.Pursue;
		yield return null;

	}

	int attempt = 0;
	private void AttackDodge( DodgeType type){


		dodgeTime += Time.deltaTime;
		// Goes in once to get the random direction ONCE until Dodging is false again.
		if (!isDodging) {
			switch (type) {
			case DodgeType.Randomize:
				dodgeDir = directions [Random.Range (0, directions.Length)];
				break;
			case DodgeType.Aggression:
				dodgeDir = player.transform.position - transform.position + new Vector3 (0, 0, 3);
				break;
			case DodgeType.Evasive:
				dodgeDir = transform.position - player.transform.position;
				break;
			}

			dodgePos = transform.position + dodgeDir;
//			if(CheckObstacle(dodgePos,wallLayer)){
//				if(attempt < 15){
//					AttackDodge(type);
//					attempt++;
//					return;
//				}
//			}
			isDodging = true;
		}

		/* Timer ------------------- */
		if (dodgeTime < dodgeDelay) {
			transform.position = Vector3.Lerp (transform.position, dodgePos, 7f * Time.deltaTime);
		} else {
			dodgeTime = 0f;
			isDodging = false;
			dodgeCount++;
		}
		
		if (dodgeCount >= dodgeLimit) {
			ResetDodge();
			//agent.Stop();
		}
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

    private void Pursue(){
		//Debug.Log ("Pursuing");
		agent.Resume ();

			switch (behavior) {

			/*-----------------------------------
		 * The PASSIVE / AGGRESSIVE Behavior 
		 * In Condition: Hunger > 50% -> Behavior One
		 * Out Condition: Hunger < 50% -> Behavior.Two
		 * ---------------------------------*/
			case Behavior.One:
			//Debug.Log ("Behavior One");
			//Debug.Log (Vector3.Distance (transform.position, player.position));

			/*--- B1S1 - String 1 Range Attacks until player is near -> String 2 ( Attack 1 ) ---*/

			// Check Distance from Player -- If Mercury is X distance from Player, Dodge/Advance and Range Attack
			if (Vector3.Distance (transform.position, player.transform.position) > meleeRange && Vector3.Distance (transform.position, player.transform.position) < detectionRange) {
					// If the Distance Between the Player and Mercury is GREATER than the meleeRange and is within the detectionRange

					// Use Range Attack
					attackType = AttackType.Range;
					state = State.Attack;
				
			} else if (Vector3.Distance (transform.position, player.transform.position) < meleeRange && !DetectWall ()) { // else if it's less than the meleeRange

					/*-----------------------------------------------------------------------------------*/

					/*--- B1S2 String 2 Melee Attacks and Dodges Away ( Attack 2 ) ---*/
					attackType = AttackType.Melee;
					state = State.Attack;

					// Goes into here to do a Melee Attack if Player is within Melee Range.

				} else {
					agent.Resume ();
					agent.SetDestination (player.transform.position);
				}

			/*-----------------------------------------------------------------------------------*/

			/*--- B1S3 String 3  The "Aggressive Mode" of Mercury. He will dash towards the Player to Land a Melee Attack and then Dodge Away ---*/

			// If Damage Dealt to Mercury is more than 10% -> Charge at Player and Attack

			// We need to store Damage Dealt. Player's Damage Dealt to Mercury within time interval over(divided by) Mercury's Current Health

			/*-----------------------------------------------------------------------------------*/

			/* B1S4 -  String 4 The "Mechanic" Super Skill 'Tornado'. He will use this after he has completed consuming a civillian.*/

			// Falls into this

				break;

			/*-----------------------------------
		 * The FEASTING / AGGRESSIVE Behavior 
		 * In Condition: Hunger < 50% -> Behavior.Two
		 * Out Condition: Hunger > 50% -> Behavior.One
		 * Priority: Aggression
		 * ---------------------------------*/
			case Behavior.Two: 
			/*--- B2S1 - String 1 Feasts while running away from Player. -> Go Back to B1S1---*/

			/*-----------------------------------------------------------------------------------*/

			/*--- B2S1 - String 2 If Player is within range, Dash Towards and Melee Attack ---*/

			/*-----------------------------------------------------------------------------------*/

			/*--- B2S1 - String 3 Range Attack the Player, then Dash towards Random Direction -> Dash Away from Player -> Range attack -> Attempt to Feast. ---*/

			/*-----------------------------------------------------------------------------------*/
				break;

			/*-----------------------------------
		 * The FEASTING / HUNGRY Behavior 
		 * In Condition: Hunger < 50% -> Behavior.Two
		 * Out Condition: Hunger > 50% -> Behavior.One
		 * Priority: Feasting
		 * ---------------------------------*/
			case Behavior.Three:

			/*--- B3S1 - String 1 Feasts while running away from Player. -> Attempt 2x---*/
			
			/*-----------------------------------------------------------------------------------*/
			
			/*--- B3S2 - String 2 If damage dealt is over 10% or Close by -> Dash Towards Player and Attack -> String 3 ---*/
			
			/*-----------------------------------------------------------------------------------*/
			
			/*--- B2S1 - String 3 Range Attack the Player, then Dash towards Random Direction -> Dash Away from Player -> Range attack -> Attempt to Feast. 2x ---*/
			
			/*-----------------------------------------------------------------------------------*/
				break;

			/*-----------------------------------
		 * The DANGER / LOW HEALTH Behavior 
		 * In Condition: Health < 10% -> Behavior.Four
		 * Out Condition: There is no OUT Condition here
		 * ---------------------------------*/
			case Behavior.Four:

			/*--- B4S1 - String 1 If Damage Dealt was more than 5%, Use Attack 3 and then Dodge Away in Random Direction 2x ---*/

			/*-----------------------------------------------------------------------------------*/

			/*--- B4S2 - String 2 If Player is within Melee Range, Melee Attack then Dodge Away 1x ---*/

			/*-----------------------------------------------------------------------------------*/

			/*--- B4S3 - String 3 Attack Player from Range & Dodge/Dash in Random Direction. ---*/

			
				break;

		}
	}

    GameObject[] fleePoints;
    GameObject fleeTarget;
    Transform fleePos;
    private float fleeDistance;
    private bool fleeing;

    void Flee() {

        fleeTarget = GameObject.FindGameObjectsWithTag("Flee")[0];
        fleeDistance = Vector3.Distance(transform.position, fleePoints[0].transform.position);

        //foreach (GameObject go in fleePoints) {
        //    if (Vector3.Distance(transform.position, go.transform.position) < fleeDistance && Vector3.Distance(player.transform.position, go.transform.position) > 20f) {
        //            fleeDistance = Vector3.Distance(transform.position, go.transform.position);
        //            fleeTarget = go;
        //    }

        //}

        

        isRun = true;

        if (!fleeing) {
            fleePos = fleePoints[Random.Range(0, fleePoints.Length)].transform;
            fleeing = true;
        }


        if(Vector3.Distance(transform.position,fleePos.position) < 5f) {
            fleeing = false;
        }

        Vector3 path = new Vector3(agent.steeringTarget.x,transform.position.y,agent.steeringTarget.z);

        //float distance = Vector3.Distance (transform.position, player.transform.position);
        Vector3 dir = (path - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);

        // Smooth Look-At Player
        if (direction < 1f) {
            Quaternion targetRotation = Quaternion.LookRotation(path - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
        }

        agent.Resume();
        agent.updateRotation = true;
        agent.SetDestination(fleePos.position);
    }

	private void Capture(){
		Debug.Log ("Capture");
		GameObject temp = null;
		float detectionRange = 2f;
		float currentDistance = 100f;

		if (target == null) {

			// Finds the closest Civillian
			foreach (GameObject c in GameObject.FindGameObjectsWithTag ("Civillian")) {

				// Store data into temp whenever the distance is less than the current.
				if (Vector3.Distance (transform.position, c.transform.position) < currentDistance) {
					currentDistance = Vector3.Distance (transform.position, c.transform.position);
					temp = c; 
				}
			}
			target = temp;

		} else { // If there is a target, then-->

			// Captures the Civillian if close enough
			agent.SetDestination (target.transform.position);

			if(Vector3.Distance (transform.position,target.transform.position) < detectionRange){
				target.GetComponent<CivillianAI>().state = CivillianAI.State.Captured;
				agent.Stop ();
				InitiateDodge(4,DodgeType.Randomize);
			}
		}
	}
	
	private IEnumerator Consume(){

		yield return null;
	}
	

	/* --------- UTILITY FUNCTION SCRIPTS ---------*/

	private void Initialize(){
		if (instance == null)
			instance = this;

		currentHunger = maxHunger;
		currentHealth = maxHealth;

		myTransform = transform;
		player = GameObject.FindGameObjectWithTag("Player");
        fleePoints = GameObject.FindGameObjectsWithTag("Flee");
        Controller = GetComponent<Animator>();

		/* Nav Mesh Properties */
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = walkSpeed; // Default is walk.
		agent.angularSpeed = rotationSpeed;
		
		/* State Machine Properties*/
		state = defaultState;
	}

	private void CheckHunger(){
		if (currentHunger < 50) { // 50%
			isHungry = true;
		} else 
			isHungry = false;
	}

	private void CheckHealth(){
		if (currentHealth <= 0) {
			isDead = true;
            gameObject.SetActive(false);
		}
	}

	// This changes the AI's Behavior based on it's hunger.
	private void UpdateAIBehavior(){

		if (currentHunger < 100 && currentHunger > 50) {
			behavior = Behavior.One;
		} else if (currentHunger < 50 && currentHunger > 35) {
			behavior = Behavior.Two;
		} else if (currentHunger < 35 && currentHunger > 25) {
			behavior = Behavior.Three;
		} else if (currentHunger < 25 && currentHunger > 0) {
			behavior = Behavior.Four;
		}
	}

	private void InitiateDodge(int amount,DodgeType type){
		dodgeLimit = amount;
		dodgeType = type;
		state = State.Dodge;

	}

	private void InitiateAttackDodge(int amount){
		dodgeLimit = amount;
	}

//	private void FindPath ()
//	{
//		agent.speed = 7;
//		agent.stoppingDistance = 5;
//		
//		if (Time.time > wanderTime) {
//			Vector3 direction = Random.insideUnitSphere * maxWalkDistance;
//			direction += transform.position;
//			NavMeshHit hit;
//			NavMesh.SamplePosition (direction, out hit, Random.Range (0f, maxWalkDistance), 1);
//			
//			Vector3 destination = hit.position;
//			agent.SetDestination (destination);
//			
//			wanderTime = Time.time + wanderDelay;
//		}
//	}

	public LayerMask wallLayer = 1 << 8;

	private bool CheckObstacle (Vector3 position, LayerMask layerMask)
	{
		
		// Checking if the the Vector3 'position' has anything it's colliding with within a radius of 2.0f and 
		// if it the item it is colliding with has the specified layerMask.
		if (Physics.CheckSphere (position, 3.0f, layerMask)) {
			Debug.Log ("Intersects a Structure.");
			return true;
		}
		return false;
	}

	RaycastHit hit;
	private bool DetectWall(){
		if (Physics.Raycast (myTransform.position, myTransform.forward, out hit)) {
			if(hit.collider.tag == "Wall"){
				Debug.Log ("Wall");
				return true;
			}else
				return false;
		}

		return false;
	}









    /* Animations Controls Here */
    void Animations() {
        ResetAnimations();

        if (isRun) {
            Controller.SetBool("Running", true);
        }
        else
            Controller.SetBool("Running", false);


        if (isIdle) {
            Controller.SetBool("Idle", true);
        }
        else
            Controller.SetBool("Idle", false);
    }


    void ResetAnimations() {
        if (currentState != state) {
            currentState = state;
            isRun = false;
            isIdle = false;
        }
    }

}
