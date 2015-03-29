//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	

	int whoAmI;
	public OVRCameraRig localCam;
	List<GameObject> fighters = new List<GameObject>();// the array of the fighters
	spawnSpot[] spots;

	// Use this for initialization

	/*void Start () {
		Debug.Log ("Start PhotonServer");
	}*/
	/*public void Connect(){
		Debug.Log("Connect to PhotonServer");
		spots= GameObject.FindObjectsOfType<spawnSpot> ();
		PhotonNetwork.ConnectUsingSettings("alpha");
	}*/

	public void ConnectAsPliot(){

		Debug.Log("ConnectAsPliot to PhotonServer");
		//spots= GameObject.FindObjectsOfType<spawnSpot> ();
		PhotonNetwork.ConnectUsingSettings("alpha");
		whoAmI = 0; //Pilot


		if(PhotonNetwork.playerList.Length>2){
			Debug.Log("more then 2 players on server - need to generate new locations");
			//Spawn new locations for new spaceship and player.
		}
	}

	public void ConnectAsGunner(){
		Debug.Log("ConnectAsGunner to PhotonServer");
		//spots= GameObject.FindObjectsOfType<spawnSpot> ();
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
		fighters.Add(PhotonNetwork.Instantiate ("fighter",Vector3.zero,Quaternion.identity,0/*group id*/));
		spots = fighters[0].GetComponentsInChildren<spawnSpot>();
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
			Debug.LogError("there are no spawnspots in the spaceship or no spaceship");
			return;
		}
		localCam.gameObject.SetActive( true);
		spawnSpot mySpot = spots [0];
		//GameObject myFighter = GameObject.Find ("Fighter");
		//myFighter.GetComponent<spaceShipController> ().amIPilot = true;
		fighters[0].GetComponent<spaceShipController> ().amIPilot = true;
		localCam.transform.parent = fighters[0].transform;
        
		localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);


	}

	void spawnGunner(){
		if (spots == null) {
			Debug.LogError("there are no spawnspots in the spaceship or no spaceship");
			return;
		}
		localCam.gameObject.SetActive( true);
		spawnSpot mySpot = spots [1];
		//GameObject myFighter = GameObject.Find ("Fighter");
		//myFighter.GetComponent<spaceShipController> ().amIPilot = true;
		fighters[0].GetComponent<spaceShipController> ().amIPilot = true;
		localCam.transform.parent = fighters[0].transform;
		
		localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);
	}

}
