using UnityEngine;
using System.Collections;

public class FighterSettings : MonoBehaviour {

	string playerName;

	void Awake(){

		playerName = PlayerPrefs.GetString("username");
	}

	public string getPlayerName(){
		return playerName;
	}


}
