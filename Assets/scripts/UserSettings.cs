using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UserSettings : MonoBehaviour {
	enum CameraDisplay{OVR,screen};
	enum ControllerType{gamepad,keyboard};

	public Toggle ovrToggle;
	public Toggle screenToggle;

	public Toggle gamepadToggle;
	public Toggle keyboardToggle;

	CameraDisplay myCam;
	ControllerType myController;
	
	void Start (){
		loadCameraSettings ();
		loadControllerSettings ();
	}





	public void useOVR(){
		myCam = CameraDisplay.OVR;

	}
	public void useScreen(){
		myCam = CameraDisplay.screen;
	}

	public void useGamePad(){
		myController = ControllerType.gamepad;
		
	}

	public void useKeyboard(){
		myController = ControllerType.keyboard;
		
	}




	public void saveAll(){
		PlayerPrefs.SetString ("cameraMode", myCam.ToString());
		PlayerPrefs.SetString ("controllerMode", myController.ToString());
		Debug.Log ("saving settings");
		PlayerPrefs.Save ();

	}
	public void restoreDefult(){
		PlayerPrefs.DeleteKey("cameraMode");
		PlayerPrefs.DeleteKey("controllerMode");
		PlayerPrefs.SetString ("cameraMode", CameraDisplay.OVR.ToString());
	}



	void loadCameraSettings(){
		if (PlayerPrefs.GetString ("cameraMode") == "") {
			Debug.Log ("cameraMode is empty, using OVR as defult");
			useOVR ();
			ovrToggle.isOn = true;
			saveAll ();
		} else {
			if(PlayerPrefs.GetString ("cameraMode")== CameraDisplay.OVR.ToString()){
				ovrToggle.isOn = true;
				screenToggle.isOn = false;
			}
			if(PlayerPrefs.GetString ("cameraMode")== CameraDisplay.screen.ToString()){
				ovrToggle.isOn = false;
				screenToggle.isOn = true;
			}
		}
	}

	void loadControllerSettings(){
		if (PlayerPrefs.GetString ("controllerMode") == "") {
			Debug.Log ("controllerMode is empty, using gamepad as defult");
			useGamePad();
			gamepadToggle.isOn = true;
			saveAll ();
		} else {
			if(PlayerPrefs.GetString ("controllerMode")== ControllerType.gamepad.ToString()){
				gamepadToggle.isOn = true;
				keyboardToggle.isOn = false;
			}
			if(PlayerPrefs.GetString ("controllerMode")== ControllerType.keyboard.ToString()){
				gamepadToggle.isOn = false;
				keyboardToggle.isOn = true;
			}
		}

	}
}
