using UnityEngine;
using System.Collections;

public class LogoutManager : MonoBehaviour
{
	private int logout_result;
	public HTTPClientLogOut logout_client;

	// Use this for initialization
	void Start ()
	{
		logout_client = GetComponent<HTTPClientLogOut> ();
		logout_result = 0;
//		Debug.Log ("about to log out user " + PlayerPrefs.GetString ("username"));
//		logout_client.Logout_POST (PlayerPrefs.GetString ("username"));
//		Debug.Log ("about out ");
	}
	
	public void UserLogout ()
	{
		//Debug.Log(PlayerPrefs.GetString ("username"));
		//logout_client.Logout_POST (PlayerPrefs.GetString ("username"));
		//logout_client.Logout_POST ("user1");
		//StartCoroutine (WaitForLogout ());
		Debug.Log ("about to log out user " + PlayerPrefs.GetString ("username"));
		logout_client.Logout_POST (PlayerPrefs.GetString ("username"));
		Debug.Log ("about out ");
	}

	private IEnumerator WaitForLogout ()
	{
		Debug.Log ("In logout attempt");
		while (logout_client.GetStatus()==false) {
			Debug.Log ("logout still waiting");
			yield return null;
		}

		logout_client.ResetStatus ();
		logout_result = logout_client.GetResultStatus ();
		Debug.Log ("out logout enu");
		Debug.Log ("time to check result");
		//Logout request result options:
		// 0 - cannot connect to server - server down/no internet
		// 1 - cannot logout - internal server error
		// 2 - logout success

		if (logout_result == 2) 
		{
			//logout was successful - proceed to updating score board
			Debug.Log("logout was successful - proceed to updating score board");
		}
		if ((logout_result == 1)||(logout_result == 0))
		{
			//logout failed - exit game
			Debug.Log("logout failed - exit game");
		}
	}
}