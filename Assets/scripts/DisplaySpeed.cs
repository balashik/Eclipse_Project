using UnityEngine;
using System.Collections;

public class DisplaySpeed : MonoBehaviour {
	public TextMesh textSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 textSpeed.text = GetComponent<fighterMotor> ().getSpeed ().ToString();
	}
}
