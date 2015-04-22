//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager :Photon.MonoBehaviour {
	


	public OVRCameraRig ovrCam;
	public Camera cam;//test cam
	public bool amIAlive;
	public float respawnTime;
	public GameObject Fighters;

	int whoAmI;
	spawnSpot[] spots;
	FighterSpawningSpot[] fighterSpots;
	int spaceshipId;
	int groupId; 
	GameObject Fighter;
	Camera[] displayCams;


	void Start(){
		amIAlive = true;
		Debug.Log ("Start");
		whoAmI = 0; //Pilot
		PhotonNetwork.ConnectUsingSettings("Alpha");
		fighterSpots = GameObject.FindObjectsOfType<FighterSpawningSpot>();

	}

	void Update(){
		if (amIAlive == false) {
			if (respawnTime > 0) {

				respawnTime -= Time.deltaTime;		
			}
			if (respawnTime <= 0) {
				spawn ();
				
			}
		}
	}

	
	void OnGUI(){
		Debug.Log (PhotonNetwork.connectionStateDetailed.ToString ());
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label ("Number of players in room "+PhotonNetwork.countOfPlayers.ToString());
		GUILayout.Label ("player ID " + PhotonNetwork.player.ID);
		if (!(Fighter == null)) {
			GUILayout.Label ("Number of players in room "+Fighter.transform.position.ToString());
		}
		if(amIAlive){
			GUI.Label (new Rect (Screen.width / 2, 10, 300, 300), "Respawn in:" + (int)respawnTime);
		}
	}

	void OnJoinedLobby(){
		Debug.Log ("OnJoinedToLobby");

		RoomOptions roomOptions = new RoomOptions (){isVisible = true};
		PhotonNetwork.JoinOrCreateRoom("mmo",roomOptions,TypedLobby.Default);
	}
	
	
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		getGroupId ();


		spawn ();

	}

	void spawn(){
		//where the fighter is spawning
		if (fighterSpots == null) {
			Debug.LogWarning ("unable spawn a Fighter to a Fighter spot, fighterSpots = null");
			return;
		}
		FighterSpawningSpot myFighterSpot = fighterSpots [Random.Range (0, fighterSpots.Length)];


		//instantiating a fighter into the game
		Fighter = PhotonNetwork.Instantiate ("NewFighter", myFighterSpot.transform.position, myFighterSpot.transform.rotation, 0);
		amIAlive = true;
		



		//spawning he player into the location in the fighter
		spots = Fighter.GetComponentsInChildren<spawnSpot>();
		if (spots == null) {
			Debug.LogWarning ("unable spawn a player to a player spot, spots = null");
			return;
		}
		spawnSpot mySpot = spots [whoAmI];
		Fighter.GetComponent<networkFighter> ().myCam = ovrCam;
		ovrCam.transform.position = mySpot.transform.position;
		ovrCam.transform.rotation = mySpot.transform.rotation;
		ovrCam.GetComponent<CameraFollow> ().SetTarget (mySpot.transform);

	}

	void getSpaceshipId(){
		if (PhotonNetwork.player.ID % 2 == 0) {
			spaceshipId = (PhotonNetwork.player.ID - 1) + 1000;
			return;
		} else {
			spaceshipId = PhotonNetwork.player.ID+1000;
			return;
		}
		
	}
	
	void getGroupId(){
		int num = PhotonNetwork.player.ID;
		if (num % 2 == 0) {
			groupId = (num / 2) - 1;
			return;
		} else {
			groupId = num / 2;
			return;
		}

	}






}
