using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public GameObject background;
	public GameObject text;
	public GameObject progressBar;

	int loadProgress;

	// Use this for initialization
	void Start () {
	
		background.SetActive(false);
		text.SetActive(false);
		progressBar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
