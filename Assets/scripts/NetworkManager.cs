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
		Debug.Log("start");
		spots= GameObject.FindObjectsOfType<spawnSpot> ();
		Connect ();
	}
	void Connect(){
		Debug.Log("Connect");
		PhotonNetwork.ConnectUsingSettings("alpha");
	}
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label (PhotonNetwork.countOfPlayers.ToString());
		GUILayout.Label (GameObject.Find("Fighter").transform.position.ToString());
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
		spawnMe ();

	}

	void spawnMe(){
		if (spots == null) {
			Debug.LogError("there are no spawnspots in the game");
			return;
		}
		spawnSpot mySpot = spots[0];

		//GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate ("OVRCameraRig",mySpot.transform.position,mySpot.transform.rotation,0/*importent spaceship id*/);
		GameObject myFighter = GameObject.Find ("Fighter");
		myFighter.GetComponent<spaceShipController> ().amIPilot = true;
		localCam.transform.parent = myFighter.transform;
		//myPlayer.GetComponent<Camera> ().enabled = true;
        localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);
		testCam.enabled = true;
        

		standbyCam.enabled = false;

	}

}
