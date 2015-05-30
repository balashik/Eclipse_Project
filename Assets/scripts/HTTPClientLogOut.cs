using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class HTTPClientLogOut : MonoBehaviour {

	private string logout_url = "http://52.24.91.179/userlogout";
	private int success_fail = -1;

	//Logout request result options:
	// 0 - cannot connect to server - server down/no internet
	// 1 - cannot logout - internal server error
	// 2 - logout success
	
	//Indicates if and when the request process has finished.
	private bool logout_request_status = false;
	
	public bool GetStatus ()
	{
		return logout_request_status;
	}

	public void ResetStatus ()
	{
		logout_request_status = false;
	}

	public int GetResultStatus ()
	{
		return success_fail;
	}

	public void Logout_POST(string username)
	{
		WWWForm form = new WWWForm();
		form.AddField("usr", username);
		
		WWW www = new WWW(logout_url, form);
		
		StartCoroutine (WaitForLogoutRequest (www));
	}

	private IEnumerator WaitForLogoutRequest (WWW www)
	{
		Debug.Log ("in wait");
		yield return StartCoroutine(ExecuteLogoutRequest(www));
		//response is JSON
		logout_request_status = true;
		//destroying username parameter
		PlayerPrefs.DeleteKey("username");
		Debug.Log ("out wait");
	}

	private IEnumerator ExecuteLogoutRequest(WWW www)
	{
		Debug.Log ("in exec");
		yield return www;
		if (www.error == null) 
		{
			if(www.text.Contains("user status was set to logout"))
			{
				success_fail = 2;
				Debug.Log("user status was set to logout   " +success_fail);
			}
			if(www.text.Contains("database query failure"))
			{
				success_fail = 1;
				Debug.Log("database query failure    "+success_fail);	
			}
		}
		else
		{
			success_fail = 0;
			Debug.Log("www error   "+success_fail);
		}
		Debug.Log ("out exec");
	}
}