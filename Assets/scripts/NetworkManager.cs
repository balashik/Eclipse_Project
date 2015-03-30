//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	

	int whoAmI;
	public OVRCameraRig localCam;
	spawnSpot[] spots;
	int groupId;
	GameObject Fighter;

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

	public void ConnectAsPliot(){

		Debug.Log("ConnectAsPliot to PhotonServer");
		PhotonNetwork.ConnectUsingSettings("alpha");
		whoAmI = 0; //Pilot
	}

	public void ConnectAsGunner(){
		Debug.Log("ConnectAsGunner to PhotonServer");
		PhotonNetwork.ConnectUsingSettings("alpha");
		whoAmI = 1; //Gunner
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label ("Number of players in room "+PhotonNetwork.countOfPlayers.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null); // need to change room name to something with value
		Debug.Log("OnPhotonJoinRoomFailed");
	}
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		getGroupId ();
		Fighter = PhotonNetwork.Instantiate ("fighter",Vector3.zero,Quaternion.identity,groupId);
		spots = Fighter.GetComponentsInChildren<spawnSpot>();
		spawn ();

	}

	void spawn(){
		if (spots == null) {
			Debug.LogError ("unable spawn to a spot, spots = null");
		}
		spawnSpot mySpot = spots [whoAmI];
		localCam.transform.parent = Fighter.transform;
		localCam.gameObject.SetActive (true);
		localCam.transform.position = mySpot.transform.position;
		localCam.transform.rotation = mySpot.transform.rotation;
		localCam.transform.position = new Vector3 (localCam.transform.position.x, 5.319f, localCam.transform.position.z);//need to change the spawn height and then to remove this line
		if (whoAmI ==0)
			Fighter.GetComponent<spaceShipController> ().amIPilot = true;
		else if (whoAmI==1)
			Fighter.GetComponent<spaceShipController> ().amIPilot = false;

	}

}
