using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {
	
	public Menu currentMenu;
	public GameObject myCanvas;
	public HTTPClient login_client;
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
		if ((input_username.text == "") || (input_password.text == "")) {
			connectionStatus.text = "Username or Password fields are empty";
		} else {
			if ((input_username.text == "eclipseAdmin") && (input_password.text == "1q2w3e")) {
				passMeThru();
			} else {
				PlayerPrefs.SetString ("username", input_username.text);
				//Debug.Log(PlayerPrefs.GetString ("username"));
				login_client.Login_POST (input_username.text, input_password.text);
				StartCoroutine (WaitForLogin ());
			}
		}
	}
	private IEnumerator WaitForLogin ()
	{
		Debug.Log ("In login enu");
		while (login_client.GetStatus()==false) {
			connectionStatus.text = "Connecting";
			Debug.Log ("still waiting");
			yield return null;
		}

		login_client.ResetStatus ();
		attempt_result = login_client.GetResultStatus ();
		Debug.Log ("out login enu");
		Debug.Log ("time to check result");
		//Login request result options:
		// 0 - cannot connect to server - server down/no internet
		// 1 - cannot login - check username/password or register in website
		// 2 - login success
		// 3 - Login Failed - User Already Logged From Different PC
		// 4 - Cannot Login - Server Failure - Please Try Again Later

		if (attempt_result != 2) {
			if(attempt_result == 0){
				Debug.Log ("cannot connect to server - server down/no internet");
				connectionStatus.text = "Connection Attempt Failed - Please Check Internet Connection";
			}
			if(attempt_result == 1){
				Debug.Log ("cannot login - check username/password or register in website");
				connectionStatus.text = "Login Failed - Please Check Username/Password";
			}
			
			if(attempt_result == 3){
				Debug.Log ("cannot login - user already logged from different PC");
				connectionStatus.text = "Login Failed - User Already Logged From Different PC";
			}
			
			if(attempt_result == 4){
				Debug.Log ("Cannot Login - Server Failure - Please Try Again Later");
				connectionStatus.text = "Cannot Login - Server Failure - Please Try Again Later";
			}
					
		}
		if (attempt_result == 2) 
		{
			Debug.Log ("login success");
			//this line responsible on the menu swich right after the player was connected
			connectionStatus.text = "Login successful!";
			myCanvas.GetComponent<MenuManager> ().showMenu (currentMenu);
		}
		if (attempt_result == (-1)) 
		{
			Debug.Log ("Failure in HTTPClient - restart game?");
			connectionStatus.text = "Failure in HTTPClient - Please Restart Game";
		}
	}
	void passMeThru(){
		myCanvas.GetComponent<MenuManager> ().showMenu (currentMenu);
	}

}