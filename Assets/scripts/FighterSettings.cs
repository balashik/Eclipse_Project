using UnityEngine;
using System.Collections;

public class FighterSettings : MonoBehaviour {

	public int teamId = -1;
	public int health  = 100;
	void Awake(){
		gameObject.tag = "Fighter";
	}

	public void addDamage(int num){
		health += num;
	}
	public int getHealth(){
		return health;
	}

}
