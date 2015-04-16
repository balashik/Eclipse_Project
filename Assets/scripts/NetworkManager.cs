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
		if (fighterSpots == null) {
			Debug.LogWarning ("unable spawn a Fighter to a Fighter spot, fighterSpots = null");
			return;
		}

		FighterSpawningSpot myFighterSpot = fighterSpots [Random.Range (0, fighterSpots.Length)];
		Fighter = PhotonNetwork.Instantiate ("Fighter", myFighterSpot.transform.position, myFighterSpot.transform.rotation, 0);
		amIAlive = true;
		
		displayCams = Fighter.GetComponentsInChildren<Camera> ();
		
		Fighter.GetComponent<networkFighter> ().displayCams = displayCams;


		spots = Fighter.GetComponentsInChildren<spawnSpot>();
		if (spots == null) {
			Debug.LogWarning ("unable spawn a player to a player spot, spots = null");
			return;
		}
		spawnSpot mySpot = spots [whoAmI];
		if (whoAmI == 0) {
			Fighter.GetComponent<networkFighter> ().amIPilot = true;
		} else {
			Fighter.GetComponent<networkFighter> ().amIPilot = false;
		}

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
