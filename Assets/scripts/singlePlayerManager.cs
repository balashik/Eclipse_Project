using UnityEngine;
using System.Collections;

public class singlePlayerManager : MonoBehaviour {

	
	public OVRCameraRig mainCam;
	public Camera[] displayCams;



	void Awake(){
				PhotonNetwork.offlineMode = true;
				foreach (Camera c in displayCams) {
						c.enabled = true;		
		
				}
				mainCam.enabled = true;
		}


}
