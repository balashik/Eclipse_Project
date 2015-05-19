using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	int kills;
	int deaths;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addKill(){
		kills++;
		Debug.Log ("num of deaths: " + kills);
	}
	public void addDeath(){
		deaths++;
		Debug.Log ("num of deaths: " + deaths);
	}
}
