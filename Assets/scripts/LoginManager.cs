using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {
	
	public Menu currentMenu;
	public GameObject myCanvas;
	HTTPClient login_client;
	public InputField input_username;
	public InputField input_password;
	private int attempt_result;
	public Text connectionStatus;

	void Start ()
	{
		input_username = input_username.GetComponent<InputField> ();
		input_password = input_password.GetComponent<InputField> ();
		login_client = GetComponent<HTTPClient> ();

		attempt_result = 0;
	}

	public void onConnectPressed(Menu menu){ //connect button action

		//login here

		login_client.POST (input_username.text, input_password.text);
		StartCoroutine (WaitForLogin ());
	}

	private IEnumerator WaitForLogin ()
	{
		Debug.Log ("In login enu");
		while (login_client.GetStatus()==false) {
			connectionStatus.text = "connecting";
			Debug.Log ("still waiting");
			yield return null;
		}

		login_client.ResetStatus ();
		attempt_result = login_client.GetResultStatus ();
		Debug.Log ("out login enu");
		Debug.Log ("time to check result");

		// 0 - cannot connect to server - server down/no internet
		// 1 - cannot login - check username/password or register in website
		// 2 - login success

		if (attempt_result != 2) {
			if(attempt_result == 0){
				Debug.Log ("cannot connect to server - server down/no internet");
				connectionStatus.text = "connection failed, please check internet connection";
			}
			if(attempt_result==1){
				Debug.Log ("cannot login - check username/password or register in website");
				connectionStatus.text = "connection failed, please check username/password";
			}
					
		}
		if (attempt_result == 2) 
		{
			Debug.Log ("login success");
			//this line responsible on the menu swich right after the player was connected
			connectionStatus.text = "connection success!";
			myCanvas.GetComponent<MenuManager> ().showMenu (currentMenu);
		}
		if (attempt_result == (-1)) 
		{
			Debug.Log ("failure in HTTPClient - restart game?");
		}
	}
}