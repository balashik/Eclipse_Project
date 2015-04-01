using UnityEngine;
using System.Collections;


public class networkFighter : Photon.MonoBehaviour {

	//public OVRCameraRig myCam;
	public Camera myCam;
	public Camera[] displayCams;
	void Update(){
		if (photonView.isMine) {
			//myCam.gameObject.SetActive(true);
			displayCams [0].enabled = true;
			displayCams [1].enabled = true; 
			displayCams [2].enabled = true;
			myCam.enabled = true;

			gameObject.GetComponent<fighterMotor>().enabled = true;
			gameObject.GetComponent<fighterGuns>().enabled = true;
		
		} else {

		}
	}
}
