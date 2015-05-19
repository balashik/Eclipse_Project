using UnityEngine;
using System.Collections;

public class Health : Photon.MonoBehaviour {
	
	public Transform explosionEffect;
	public int health = 100;
	GameObject MyScore;
	void Start(){
	
		MyScore = GameObject.Find("ScoreManager");
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
		MyScore.GetComponent<Score> ().addDeath ();
		Debug.Log ("destroy");
		Instantiate(explosionEffect,transform.position,transform.rotation);
		if(gameObject.GetComponent<PhotonView>().instantiationId==0){
			Destroy(gameObject);
		}else{
			if(GetComponent<PhotonView>().isMine){
				if (gameObject.tag=="Fighter"){
					GameObject.Find("OVRCameraRig");
					NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
					nm.amIAlive = false;
					nm.respawnTime = 10f;
				}
				PhotonNetwork.Destroy (gameObject);
			}
		}
		
	}
}
