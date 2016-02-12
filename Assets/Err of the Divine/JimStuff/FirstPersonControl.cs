using UnityEngine;
using System.Collections;

public class FirstPersonControl : MonoBehaviour {

	public float movementSpeed = 5.0f;
	private float movementSpeedReturn = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 7.0f;

	public float runSpeed = 20.0f;
	public float crouchSpeed = 1.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;

	float verticalVelocity = 0;

	CharacterController characterController;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation

		float rotHorizontal = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotHorizontal, 0);

		//float rotVertical = Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		//Camera.main.transform.Rotate (-rotVertical, 0, 0);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//X and Z movement
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		//Crouch
		if(characterController.isGrounded && Input.GetButtonDown("Crouch"))
		{
			movementSpeed = crouchSpeed;
			//this.transform.position = Vector3.up * 0.5f;
			//This won't work since it like resetting, (0,1,0) * 5 again and again.

		}
		else if(characterController.isGrounded && Input.GetButtonUp("Crouch"))
		{
			movementSpeed = movementSpeedReturn;
		}


		//Sprint
		if(characterController.isGrounded && Input.GetButtonDown("Sprint"))
		{
			movementSpeed = runSpeed;
		}
		else if(characterController.isGrounded && Input.GetButtonUp("Sprint"))
		{
			movementSpeed = movementSpeedReturn;
		}

		//Why both GetButtonDown and GetButtonUp with if and ElseIf?
		//Cause Unity is Stupid that's why.

		//Jump
		if (characterController.isGrounded && Input.GetButton ("Jump")) 
		{
			verticalVelocity = jumpSpeed;
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		//CharacterController ChaCont = GetComponent<CharacterController> ();

		characterController.Move (speed * Time.deltaTime);

	}
}
