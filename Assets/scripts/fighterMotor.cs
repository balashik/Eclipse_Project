using UnityEngine;
using System.Collections;

public class fighterMotor : MonoBehaviour {


	public thrusters[] myThrusters;
	public float acceleration; //spaceship accelleration multiplaier
	public float roleRate; //spaceship role multiplier
	public float yawRate;	//spaceship yaw multiplier (barrel role)
	public float pitchRate; //spaceship pitch multiplier (up/down)
	Rigidbody myRigidBody;

	float speed;
	// Use this for initialization
	void Start () {

		myRigidBody = rigidbody;
		if (myRigidBody == null) {
			Debug.LogError("Spaceship has no rigidbody - the thruster scripts will fail. Add rigidbody component to the spaceship.");
		}
		if (myThrusters == null) {
			Debug.LogError ("Thruster array not properly configured. Attach thrusters to the game object and link them to the Thrusters array.");
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (speed > 0) {
			/*foreach (thrusters thruster in myThrusters) {
				thruster.StartThruster ();
				
			}*/
			gameObject.GetComponent<PhotonView> ().RPC("increaseThrust",PhotonTargets.All);
		}
		if (speed<= 0) {
			/*foreach (thrusters thruster in myThrusters) {
				thruster.StopThruster ();
			}*/
			gameObject.GetComponent<PhotonView> ().RPC("decreaseThrust",PhotonTargets.All);
		}

	}

	void FixedUpdate(){
		if (PlayerPrefs.GetString ("controllerMode") == "gamepad") {
			gamepadControll();
		}
		if (PlayerPrefs.GetString ("controllerMode") == "keyboard") {
			keyboardControll();	
		}
		if (PlayerPrefs.GetString ("controllerMode") == "joystick") {
			joystickControll();	
		}
	}

	void gamepadControll(){

		speed = Input.GetAxis ("Speed") * acceleration * -1;
		Debug.Log (Input.GetAxis ("Speed"));
		if (speed >= 1) {
			speed = 1f;		
		}
		if (speed <= -0.5) {
			speed = -0.5f;
		}
		myRigidBody.AddRelativeTorque (new Vector3 (0, 0, -Input.GetAxis ("Horizontal") * roleRate * myRigidBody.mass));
		myRigidBody.AddRelativeTorque (new Vector3 (0, Input.GetAxis ("Horizontal") * yawRate * myRigidBody.mass, 0));
		myRigidBody.AddRelativeTorque (new Vector3 (Input.GetAxis ("Vertical") * pitchRate * myRigidBody.mass, 0, 0));
		myRigidBody.velocity += transform.forward * speed;
	}


	void keyboardControll(){
		if (Input.GetKey (KeyCode.Space)) {
			speed +=(Time.deltaTime* acceleration);
			if (speed >= 1) {
				speed = 1f;
			}
		}
		if (Input.GetKey(KeyCode.N)){
			speed=0;
		}
		if (Input.GetKey (KeyCode.X)) {
			speed -=(Time.deltaTime* acceleration);
			if (speed >= 1) {
				speed = 1f;		
			}
		}
		if (speed <= -0.5) {
			speed = -0.5f;
		}

		 
		myRigidBody.AddRelativeTorque (new Vector3 (0, 0, -Input.GetAxis ("HorizontalKeyboard") * roleRate * myRigidBody.mass));
		myRigidBody.AddRelativeTorque (new Vector3 (0, Input.GetAxis ("HorizontalKeyboard") * yawRate * myRigidBody.mass, 0));
		myRigidBody.AddRelativeTorque (new Vector3 (Input.GetAxis ("VerticalKeyboard") * pitchRate * myRigidBody.mass, 0, 0));
		myRigidBody.velocity += transform.forward * speed;
	}

	void joystickControll(){
		Debug.Log("enter here");
		speed = Input.GetAxisRaw("SpeedJoyStick");
		//speed = (Input.GetAxis ("SpeedJoyStick")*acceleration);
		//speed = Input.GetAxisRaw ("SpeedJoyStick");
		//Debug.Log (Input.GetAxisRaw("VerticalJoyStick"));
		//myRigidBody.velocity += transform.forward * speed;

		myRigidBody.AddRelativeTorque (new Vector3 (0, 0, -Input.GetAxis ("XJoyStick") * roleRate * myRigidBody.mass));
		//myRigidBody.AddRelativeTorque (new Vector3 (0, Input.GetAxis ("YJoyStick") * yawRate * myRigidBody.mass, 0));
		myRigidBody.AddRelativeTorque (new Vector3 (Input.GetAxis ("YJoyStick") * pitchRate * myRigidBody.mass, 0, 0));
		myRigidBody.velocity += transform.forward * speed;

	}
	public int getSpeed(){
		return (int)(speed * 10000);
	}

	[RPC]
	void increaseThrust(){
		foreach (thrusters thruster in myThrusters) {
			thruster.StartThruster ();
		}
	}

	[RPC]
	void decreaseThrust(){
		foreach (thrusters thruster in myThrusters) {
			thruster.StopThruster ();
		}
	}





}
