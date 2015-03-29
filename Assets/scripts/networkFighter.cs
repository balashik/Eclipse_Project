using UnityEngine;
using System.Collections;

public class networkFighter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	float lastUpdateTime = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			/*if (photonView.isMine) {
				Debug.Log ("photon is mine");
			} else {
				Debug.Log ("photon is not mine");
				transform.position = Vector3.Lerp(transform.position,realPosition,lastUpdateTime);
				transform.rotation = Quaternion.Lerp(transform.rotation,realRotation,lastUpdateTime);
			}*/				
				
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

		if (gameObject.GetComponent<spaceShipController> ().amIPilot) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

		} else {
			transform.position = (Vector3) stream.ReceiveNext();
			transform.rotation = (Quaternion) stream.ReceiveNext();

			}


	}
}
