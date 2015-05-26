using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	int kills;
	int deaths;

	// Update is called once per frame
	void Update () {
		
	}

	public void addKill(){
		kills++;
		Debug.Log ("num of kills: " + kills);
	}
	public void addDeath(){
		deaths++;
		Debug.Log ("num of deaths: " + deaths);
	}

	public int getKills(){
		return kills;
	}
	public int getDeath(){
		return deaths;
	}

}
