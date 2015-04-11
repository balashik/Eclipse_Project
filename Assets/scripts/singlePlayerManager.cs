using UnityEngine;
using System.Collections;

public class singlePlayerManager : MonoBehaviour {

	
	public OVRCameraRig mainCam;
	public Camera[] displayCams;



	void Awake(){
				
				foreach (Camera c in displayCams) {
						c.enabled = true;		
		
				}
				mainCam.enabled = true;
		}


}
