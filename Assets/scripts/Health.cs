using UnityEngine;
using System.Collections;

public class Health : Photon.MonoBehaviour {
	
	public Transform explosionEffect;
	public int health = 100;
	GameObject MyScore;
	void Start(){
	
		MyScore = GameObject.Find("ScoreManager");
	}
	void Update(){
	}
	[RPC]
	public void addDamage(int num){
		health += num;
		if (health <= 0) {
			destroyFighter();		
		}
	}
	public int getHealth(){
		return health;
	}
	
	public void destroyFighter(){

		Debug.Log ("destroy");
		Instantiate(explosionEffect,transform.position,transform.rotation);
		if (gameObject.GetComponent<PhotonView> () == null) {
			Debug.Log ("destroyed object is without photonview");
						Destroy (gameObject);		
		} else {
			if(gameObject.GetComponent<PhotonView>().instantiationId==0){
				Destroy(gameObject);
			}else{
				if(GetComponent<PhotonView>().isMine){
					Debug.Log("entered is Mine");
					MyScore.GetComponent<Score> ().addDeath ();
					if (gameObject.tag=="Fighter"){
						GameObject.Find("OVRCameraRig");
						NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
						if (nm==null){
							singlePlayerManager sm = GameObject.FindObjectOfType<singlePlayerManager>();
							sm.amIAlive = false;
							sm.respawnTime = 10f;

						}
						else{
							nm.amIAlive = false;
							nm.respawnTime = 10f;
						}

					}
					PhotonNetwork.Destroy (gameObject);
				}
			}
		}

		
	}
}
