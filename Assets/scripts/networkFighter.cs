using UnityEngine;
using System.Collections;


public class networkFighter : Photon.MonoBehaviour {

	//public OVRCameraRig myCam;
	public Camera myCam;
	void Start(){

	}
	void Update(){
		if (photonView.isMine) {
			myCam.enabled = true;
			gameObject.GetComponent<fighterMotor>().enabled = true;
			gameObject.GetComponent<fighterGuns>().enabled = true;
		
		} else {
		}
	}
}
