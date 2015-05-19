using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ApplyDamage : MonoBehaviour {

	//Drag this class on the object you want to modify player's health!

	//This handles subtracting and adding health points to player in a convenient way.
	/*As follows:
	 * Tick 'Is this healing player' true or false in the inspector (consequently it will add or subtract health points)
	 * Declare amount (on a 1000 scale not on 100!)
	 * Add a collider or trigger collider depending on your needs. See below the proper sections for different
	 * behaviors!
	 * */


	public bool IsThisHealingPlayer;
	public int AmountOfHealth;

	int health;
	Healthbar healthbar;

	void Start(){

		healthbar = FindObjectOfType<Healthbar>();
		if (healthbar == null) {
			Debug.LogError("Healthbar class is not found in scene!");
		}

		if (IsThisHealingPlayer) 
			health = AmountOfHealth * -1;
		else
			health = AmountOfHealth * 1;

	}

	//Use tags for different objects to make a difference. (so that for example fire will do zone damage, but medicine box pickup
	//will refill health in an instance.

	//Trigger collider:
	//For zone damage (runs every frame and damages/heals player when inside trigger zone (eg. fire, radiation field, etc)
	void OnTriggerStay(Collider other) {
		if (other.transform.tag == "Player") {
			healthbar.SendMessage("ModifyHealth", health, SendMessageOptions.DontRequireReceiver);
		}
	}

	//Trigger collider:
	//For picking up medicine boxes or health potions (enable Destroy function to remove the item after being picked up) (
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			healthbar.SendMessage("ModifyHealth", health, SendMessageOptions.DontRequireReceiver);
			//Destroy(gameObject);
		}
	}	

	//Plain collider (eg. sphere for bullet)
	//This is to damage player on impact (rock falling, bullet hitting, etc)
	//Note: Check collision matrix for ControllerColliderHit, also, colliding with character controller is a bit more
	//complex so modify this section according to your needs.
	void OnControllerColliderHit(ControllerColliderHit other) {
		if (other.transform.tag == "Player") {
			healthbar.SendMessage("ModifyHealth", health, SendMessageOptions.DontRequireReceiver);
		}
	}

	/* This is to be used if the player is a rigidbody (eg. rollerball)
	void OnCollisionEnter(Collision other) {
		if (other.transform.tag == "Player") {
			healthbar.SendMessage("ModifyHealth", health, SendMessageOptions.DontRequireReceiver);
		}
	}*/
}
