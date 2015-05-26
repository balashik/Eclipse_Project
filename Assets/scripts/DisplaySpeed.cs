using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplaySpeed : MonoBehaviour {
	public Text speedText;
	public Text healthText;
	public Text kills;
	public Text deaths;
	Score playerScore;
	// Use this for initialization
	void Start () {
	
		playerScore = GameObject.Find("ScoreManager").GetComponent<Score>();

	}
	
	// Update is called once per frame
	void Update () {
		speedText.text = GetComponent<fighterMotor> ().getSpeed ().ToString();
		healthText.text = GetComponent<Health> ().getHealth ().ToString ();
		kills.text = "Kills: " + playerScore.getKills ();
		deaths.text = "Deaths: " + playerScore.getDeath ();
		if (GetComponent<Health> ().getHealth () > 85) {
			healthText.color = Color.green;		
		}
		if ((GetComponent<Health> ().getHealth () <= 85)&& (GetComponent<Health> ().getHealth () >50)){
			healthText.color = Color.yellow;	
		}
		if ((GetComponent<Health> ().getHealth () <= 50)&& (GetComponent<Health> ().getHealth () >25)){
			healthText.color = new Color(255f,140f,0f);
		}
		
		if ((GetComponent<Health> ().getHealth () <= 25) && (GetComponent<Health> ().getHealth () > 0)) {
			healthText.color = Color.red;	
		}
		

	}
}
