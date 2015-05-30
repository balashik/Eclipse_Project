using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	public Transform explosionEffect;
	public GameObject Fighter;
	public Menu endGameMenu;
	public Text kills;
	public Text deaths;
	public Text ttl;
	public GameObject scoreManager;
	public LogoutManager lg_m;

	float time = 15;
	// Use this for initialization
	void Start () {

		if (Fighter == null) {
			Debug.Log ("no fighter connected");		
		} else {
			destroyFighter ();
			//printing username parameter
			Debug.Log (PlayerPrefs.GetString("username"));
			//destroying username parameter
			//PlayerPrefs.DeleteKey("username");
		}

		endGameMenu.setIsOpen (true);
		lg_m.UserLogout ();
		kills.text = "kills: "+ scoreManager.GetComponent<Score>().getKills();
		deaths.text = "deaths: "+ scoreManager.GetComponent<Score>().getDeath();
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		ttl.text = "exiting game in : " + (int)time;
		if (time <= 0) {
			Application.Quit();	
		}
	}

	 void destroyFighter(){
		Instantiate (explosionEffect, transform.position, transform.rotation);
		if (Fighter.GetComponent<PhotonView> ().instantiationId == 0) {
			Destroy (Fighter);
		} else {
			if (Fighter.GetComponent<PhotonView> ().isMine) {
				PhotonNetwork.Destroy (Fighter);
			}
		}
	}

	public void timeToLeaveGame(){
		float time = 10;
	}
}