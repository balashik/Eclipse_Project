//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standbyCam;
	spawnSpot[] spots;
	int whoAmI;
	public OVRCameraRig localCam;
	public Camera testCam;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start PhotonServer");
		//spots= GameObject.FindObjectsOfType<spawnSpot> ();
		//Connect ();
	}
	public void Connect(){
		Debug.Log("Connect to PhotonServer");
		spots= GameObject.FindObjectsOfType<spawnSpot> ();
		PhotonNetwork.ConnectUsingSettings("alpha");
	}

	public void ConnectAsPliot(){
		Debug.Log("ConnectAsPliot to PhotonServer");
		spots= GameObject.FindObjectsOfType<spawnSpot> ();
		PhotonNetwork.ConnectUsingSettings("alpha");
		whoAmI = 0; //Pilot
		
		if(PhotonNetwork.playerList.Length>2)
		{
			Debug.Log("more then 2 players on server - need to generate new locations");
			//Spawn new locations for new spaceship and player.
		}
	}

	public void ConnectAsGunner(){
		Debug.Log("ConnectAsGunner to PhotonServer");
		spots= GameObject.FindObjectsOfType<spawnSpot> ();
		PhotonNetwork.ConnectUsingSettings("alpha");
		whoAmI = 1; //Gunner
		
		if(PhotonNetwork.playerList.Length>2)
		{
			Debug.Log("more then 2 players on server - need to generate new locations");
			//Spawn new locations for new spaceship and player.
		}
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label ("Number of players in room "+PhotonNetwork.countOfPlayers.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		//PhotonNetwork.JoinOrCreateRoom ("mmo",null,null);	
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null);
		Debug.Log("OnPhotonJoinRoomFailed");
	}
	void OnJoinedRoom(){
		Debug.Log("OnJoinedRoom");
		Debug.Log ("Checking which player to spawn");

		if (whoAmI==0) {
			spawnPilot ();
			Debug.Log ("spawned pilot");
		} 
		else {
			spawnGunner ();
			Debug.Log ("spawned gunner");
				}
	}

	void spawnPilot(){
		if (spots == null) {
			Debug.LogError("there are no spawnspots in the game");
			return;
		}
		spawnSpot mySpot = spots[0];

		GameObject myFighter = GameObject.Find ("Fighter");
		myFighter.GetComponent<spaceShipController> ().amIPilot = true;
		localCam.transform.parent = myFighter.transform;
        
		localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);

		testCam.enabled = true;
        standbyCam.enabled = false;
	}

	void spawnGunner(){
		if (spots == null) {
			Debug.LogError("there are no spawnspots in the game");
			return;
		}
		spawnSpot mySpot = spots[1];
		
		GameObject myFighter = GameObject.Find ("Fighter");
		myFighter.GetComponent<spaceShipController> ().amIPilot = false;

		localCam.transform.parent = myFighter.transform;
		localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);

		testCam.enabled = true;
		standbyCam.enabled = false;
	}

}
