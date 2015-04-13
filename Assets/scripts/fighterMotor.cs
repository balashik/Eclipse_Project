using UnityEngine;
using System.Collections;

public class fighterMotor : MonoBehaviour {


	public thrusters[] myThrusters;
	public float acceleration; //spaceship accelleration multiplaier
	public float roleRate; //spaceship role multiplier
	public float yawRate;	//spaceship yaw multiplier (barrel role)
	public float pitchRate; //spaceship pitch multiplier (up/down)
	Rigidbody myRigidBody;
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

		if (Input.GetAxis ("Speed") * acceleration * -1 > 0) {
			foreach (thrusters thruster in myThrusters) {
				thruster.StartThruster ();
				
			}
		}
		if (Input.GetAxis ("Speed") * acceleration * -1 <= 0) {
			foreach (thrusters thruster in myThrusters) {
				thruster.StopThruster ();
			}
		}
	
	}

	void FixedUpdate(){
		float speed = Input.GetAxis ("Speed") * acceleration * -1;
		if (speed >= 1.5) {
			speed=1.5f;		
		}
		if (speed <= -1) {
			speed=-1f;
		}
		myRigidBody.AddRelativeTorque (new Vector3 (0, 0, -Input.GetAxis ("Horizontal") * roleRate * myRigidBody.mass));
		myRigidBody.AddRelativeTorque (new Vector3 (0, Input.GetAxis ("Horizontal") * yawRate * myRigidBody.mass, 0));
		myRigidBody.AddRelativeTorque (new Vector3 (Input.GetAxis ("Vertical") * pitchRate * myRigidBody.mass, 0, 0));
		
		
		myRigidBody.velocity += transform.forward * (speed);
	}
}
