using UnityEngine;
using System.Collections;

public enum playerRole{pilot,gunner}
public enum gametype{singleplayer,multiplayer}

public class PreLevelSettings : MonoBehaviour {

	playerRole myPlayerRole;
	gametype myGameType;
	
	playerRole getPlayerRole(){
		return myPlayerRole;
	}

	void setplayerRole(playerRole value){
		myPlayerRole = value;
	}

	playerRole getGameType(){
		return myPlayerRole;
	}

	void setGameType(gametype value){
		myGameType = value;
	}


}
