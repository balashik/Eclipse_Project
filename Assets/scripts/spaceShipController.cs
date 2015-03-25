using UnityEngine;
using System.Collections;

public class spaceShipController : MonoBehaviour {

	public thrusters[] myThrusters;
	public float acceleration; //spaceship accelleration multiplaier
	public float roleRate; //spaceship role multiplier
	public float yawRate;	//spaceship yaw multiplier (barrel role)
	public float pitchRate; //spaceship pitch multiplier (up/down)
	public Rigidbody gunAimRigidBody;
	public float yMouseTop;
	public float yMouseBottom;
	bool addForceAtPosition = false;
	Rigidbody myRigidBody;
	public bool amIPilot;
	
	public int shotBuffer;
	public Vector3[] gunnerMountPoints; //where we are shooting from
	public Transform gunnerShotPrefab;

	float mouseY;
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
	int counter=0;
	// Update is called once per frame
	void Update () {
		if (!amIPilot) {
						if ((Input.GetAxis ("leftGun") == 1)) {
								counter++;
								Debug.Log ("counter is " + counter);
								if (counter == shotBuffer) {
										// Calculate where the position is in world space for the mount point
										Vector3 pos = transform.position + transform.right * gunnerMountPoints [1].x + transform.up * gunnerMountPoints [1].y + transform.forward * gunnerMountPoints [1].z;
										// Instantiate the laser prefab at position with the spaceships rotation
										Transform gunShot = (Transform)Instantiate (gunnerShotPrefab, pos, transform.rotation);
										// Specify which transform it was that fired this round so we can ignore it for collision/hit
										gunShot.GetComponent<SU_LaserShot> ().firedBy = transform;
										counter = 0;
					
								}
						}
						if ((Input.GetAxis ("rightGun") == 1)) {
								counter++;
								Debug.Log ("counter is " + counter);
								if (counter == shotBuffer) {
										// Calculate where the position is in world space for the mount point
										Vector3 pos = transform.position + transform.right * gunnerMountPoints [0].x + transform.up * gunnerMountPoints [0].y + transform.forward * gunnerMountPoints [0].z;
										// Instantiate the laser prefab at position with the spaceships rotation
										Transform gunShot = (Transform)Instantiate (gunnerShotPrefab, pos, transform.rotation);
										// Specify which transform it was that fired this round so we can ignore it for collision/hit
										gunShot.GetComponent<SU_LaserShot> ().firedBy = transform;
										counter = 0;
					
								}
						}
						if ((Input.GetAxis ("leftGun") == 1) && (Input.GetAxis ("rightGun") == 1)) {
							counter++;
							Debug.Log ("counter is " + counter);
							if (counter == shotBuffer) {
								foreach (Vector3 gun in gunnerMountPoints) {
									// Calculate where the position is in world space for the mount point
									Vector3 pos = transform.position + transform.right * gun.x + transform.up * gun.y + transform.forward * gun.z;
									// Instantiate the laser prefab at position with the spaceships rotation
									Transform gunShot = (Transform)Instantiate (gunnerShotPrefab, pos, transform.rotation);
									// Specify which transform it was that fired this round so we can ignore it for collision/hit
									gunShot.GetComponent<SU_LaserShot> ().firedBy = transform;
									counter = 0;
						
								}
							}
						}
				
			mouseY = gunAimRigidBody.transform.position.y+Input.GetAxis("Mouse Y");
			if (mouseY < -yMouseTop) {
				mouseY = -yMouseTop;
				
			}
			if (mouseY > yMouseBottom) {
				mouseY = yMouseBottom;
				
			}
			gunAimRigidBody.transform.position = new Vector3(gunAimRigidBody.transform.position.x+Input.GetAxis("Mouse X"),mouseY,gunAimRigidBody.transform.position.z);
			//gunAimRigidBody.transform.position = new Vector3(transform.position.x+Input.GetAxis("Mouse X"),transform.position.y,transform.position.z);
			//gunAimRigidBody.MovePosition(transform.position+Input.GetAxis ("Mouse Y"));
			//Debug.Log ("x"+Input.GetAxis("Mouse X"));
		
		}
						
		if (amIPilot) {
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
	


	}
	void FixedUpdate(){
		if (amIPilot) {

						myRigidBody.AddRelativeTorque (new Vector3 (0, 0, -Input.GetAxis ("Horizontal") * roleRate * myRigidBody.mass));
						myRigidBody.AddRelativeTorque (new Vector3 (0, Input.GetAxis ("Horizontal") * yawRate * myRigidBody.mass, 0));
						myRigidBody.AddRelativeTorque (new Vector3 (Input.GetAxis ("Vertical") * pitchRate * myRigidBody.mass, 0, 0));


						myRigidBody.velocity += transform.forward * (Input.GetAxis ("Speed") * acceleration * -1);
						/*if (Input.GetKeyDown (KeyCode.UpArrow)) {
						myRigidBody.velocity = transform.TransformDirection (new Vector3 (0, 0, acceleration));
				}*/
				}
	}
}
