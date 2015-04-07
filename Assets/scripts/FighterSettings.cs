using UnityEngine;
using System.Collections;

public class FighterSettings : MonoBehaviour {

	public int teamId = -1;
	void Awake(){
		gameObject.tag = "Fighter";
	}

}
