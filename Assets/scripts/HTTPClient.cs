using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class HTTPClient : MonoBehaviour {

	private string url = "http://52.24.91.179/";

	private int success_fail = -1;
	// 0 - cannot connect to server - server down/no internet
	// 1 - cannot login - check username/password or register in website
	// 2 - login success

	//Indicates if and when the request process has finished.
	private bool request_status = false;


	public bool GetStatus ()
	{
		return request_status;
	}

	public void ResetStatus ()
	{
		request_status = false;
	}

	public int GetResultStatus ()
	{
		return success_fail;
	}

	public void POST(string username, string passw)
	{
		WWWForm form = new WWWForm();
		form.AddField("usr", username);
		form.AddField("pass", passw);

		WWW www = new WWW(url, form);

		StartCoroutine (WaitForRequest (www));
	}

	private IEnumerator WaitForRequest (WWW www)
	{
		Debug.Log ("in wait");
		yield return StartCoroutine(ExecuteRequest(www));
		//response is JSON
		request_status = true;
		Debug.Log ("out wait");
	}

	private IEnumerator ExecuteRequest(WWW www)
	{
		Debug.Log ("in exec");
		yield return www;
		if (www.error == null)
		{
			if(www.text.Contains("user exists"))
			{
				success_fail = 2;
				Debug.Log(success_fail);
			}
			else
			{
				success_fail = 1;
				Debug.Log(success_fail);
			}
		} 
		else {
			success_fail = 0;
			Debug.Log("www error"+success_fail);
		}
		Debug.Log ("out exec");
	}
}