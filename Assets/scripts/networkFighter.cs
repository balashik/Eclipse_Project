using UnityEngine;
using System.Collections;


public class networkFighter : Photon.MonoBehaviour {

	//public OVRCameraRig myCam;
	public Camera myCam;
	public Camera[] displayCams;
	public bool amIPilot;
	void Update(){
		if (photonView.isMine) {
			//myCam.gameObject.SetActive(true);
			displayCams [0].enabled = true;
			displayCams [1].enabled = true; 
			displayCams [2].enabled = true;
			myCam.enabled = true;
			if(amIPilot){
				gameObject.GetComponent<fighterMotor>().enabled = true;
			}
			else{
				Debug.Log("I AM GUNNERRRRRRRRRRRRRRRRRRRRRRR");
				gameObject.GetComponent<fighterGuns>().enabled = true;
			}

		
		} else {

		}
	}


	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
			//stream.SendNext (gameObject.GetComponent<FighterSettings> ().teamId);		
		
		} else {

		}

	}
}
