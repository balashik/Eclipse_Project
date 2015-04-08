//Project EcliPse - Shenkar final project 2015.
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

	/*GameObject findCurrentFighter(){
		Debug.Log("Testttttttttttttttttttttttttttttttttttttttttttttttt");
		return Fighters.GetComponent<FightersArray> ().fightersList [0];
	}*/

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
		GUILayout.Label ("player ID " + PhotonNetwork.player.ID);
		if (!(Fighter == null)) {
			GUILayout.Label ("Number of players in room "+Fighter.transform.position.ToString());		
		}
//		
	}

	void OnJoinedLobby(){

		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null); // need to change room name to something with value
		Debug.Log("OnPhotonJoinRoomFailed");
	}
	void OnCreateRoom(){
		Debug.Log ("OnCreateRoom");
	}
	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		getGroupId ();

		if ((PhotonNetwork.player.ID % 2) != 0) {
			Debug.Log("odd");

			photonView.RPC("createFighter",PhotonTargets.AllBuffered);

			//GameObject fighterPrefab = (GameObject)Resources.Load("fighter");



			//Fighter = PhotonNetwork.Instantiate ("fighter", Vector3.zero, Quaternion.identity,/*groupId*/0);

			//gal.Add(Fighter);

			//Fighter = PhotonNetwork.InstantiateSceneObject("fighter",Vector3.zero,Quaternion.identity,0,null);
			//photonView.RPC("setIdtoFighter",PhotonTargets.All,null);
			//Fighter.GetComponent<FighterSettings>().groupId = groupId;
			Debug.Log (GameObject.Find("Fighter(Clone)").ToString());

		} else {
			Debug.Log("even");
			StartCoroutine(WaitForFighter());
			//Debug.Log(Fighters.GetComponent<FightersArray>().fightersList.Count);

		}
		//Debug.Log (Fighter);
		/*displayCams = Fighter.GetComponentsInChildren<Camera> ();
		displayCams [0].enabled = false;
		displayCams [1].enabled = false;
		displayCams [2].enabled = false;

		spawn ();*/

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
		cam.transform.position = mySpot.transform.position;
		cam.transform.rotation = mySpot.transform.rotation;
		
		cam.transform.position = new Vector3 (ovrCam.transform.position.x, 5.319f, ovrCam.transform.position.z);//need to change the spawn height and then to remove this line
		Fighter.GetComponent<networkFighter> ().myCam = cam;
		Fighter.GetComponent<networkFighter> ().displayCams = displayCams;
		cam.GetComponent<CameraFollow> ().SetTarget (mySpot.transform);

		/*oculus section
		ovrCam.transform.position = mySpot.transform.position;
		ovrCam.transform.rotation = mySpot.transform.rotation;

		ovrCam.transform.position = new Vector3 (ovrCam.transform.position.x, 5.319f, ovrCam.transform.position.z);//need to change the spawn height and then to remove this line
		Fighter.GetComponent<networkFighter> ().myCam = ovrCam;
		ovrCam.GetComponent<CameraFollow> ().SetTarget (mySpot.transform);
*/
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
