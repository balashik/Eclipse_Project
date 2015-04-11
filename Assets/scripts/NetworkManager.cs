﻿//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager :Photon.MonoBehaviour {
	

	int whoAmI;
	public OVRCameraRig ovrCam;
	public Camera cam;//test cam
	spawnSpot[] spots;
	int spaceshipId;
	int groupId; 
	GameObject Fighter;
	Camera[] displayCams;
	public GameObject Fighters;
	
	void Awake(){


	}
	void Start(){
		ovrCam.camera.enabled = false;
		Debug.Log ("Start");
		whoAmI = 0; //Pilot
		PhotonNetwork.ConnectUsingSettings("Alpha");
	}

	
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label ("Number of players in room "+PhotonNetwork.countOfPlayers.ToString());
		GUILayout.Label ("player ID " + PhotonNetwork.player.ID);
		if (!(Fighter == null)) {
			GUILayout.Label ("Number of players in room "+Fighter.transform.position.ToString());		
		}
//		
	}

	void OnJoinedLobby(){
		Debug.Log ("OnJoinedToLobby");

		RoomOptions roomOptions = new RoomOptions (){isVisible = true};
		PhotonNetwork.JoinOrCreateRoom("mmo",roomOptions,TypedLobby.Default);
	}
	

	/*void OnJoinedLobby(){

		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null); // need to change room name to something with value
		Debug.Log("OnPhotonJoinRoomFailed");
	}
	void OnCreateRoom(){
		Debug.Log ("OnCreateRoom");
	}*/
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		getGroupId ();


		Fighter = PhotonNetwork.Instantiate ("Fighter", Vector3.zero, Quaternion.identity, 0);

		displayCams = Fighter.GetComponentsInChildren<Camera> ();

		Fighter.GetComponent<networkFighter> ().displayCams = displayCams;

		spawn ();

	}

	void spawn(){

		spots = Fighter.GetComponentsInChildren<spawnSpot>();
		if (spots == null) {
			Debug.LogError ("unable spawn to a spot, spots = null");
		}
		spawnSpot mySpot = spots [whoAmI];
		if (whoAmI == 0) {
			Fighter.GetComponent<networkFighter> ().amIPilot = true;
		} else {
			Fighter.GetComponent<networkFighter> ().amIPilot = false;
		}

		/*
		cam.transform.position = mySpot.transform.position;
		cam.transform.rotation = mySpot.transform.rotation;
		Fighter.GetComponent<networkFighter> ().myCam = cam;
		Fighter.GetComponent<networkFighter> ().displayCams = displayCams;
		cam.GetComponent<CameraFollow> ().SetTarget (mySpot.transform);
*/
		//oculus section

		ovrCam.transform.position = mySpot.transform.position;
		ovrCam.transform.rotation = mySpot.transform.rotation;


		Fighter.GetComponent<networkFighter> ().myCam = ovrCam;
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
		Debug.Log (groupId);
	}

	IEnumerator WaitForFighter()
	{
		Debug.Log ("entered corutine");
		while (Fighters.GetComponent<FightersArray>().fightersList.Count==0) {
				
			yield return new WaitForSeconds(0.5f);
		}

		Debug.Log ("Corutine finished");
		Fighter = Fighters.GetComponent<FightersArray>().fightersList[0];

		displayCams = Fighter.GetComponentsInChildren<Camera> ();
		displayCams [0].enabled = false;
		displayCams [1].enabled = false;
		displayCams [2].enabled = false;
		
		spawn ();

	}

	[RPC]
	public void createFighter(){
		GameObject fighterPrefab = (GameObject)Resources.Load("fighter");

		Fighter = Instantiate (fighterPrefab,Vector3.zero,Quaternion.identity)as GameObject;
		Fighter.GetComponent<FighterSettings>().teamId = groupId;

		Fighters.GetComponent<FightersArray> ().fightersList.Add (Fighter);

	}


}
