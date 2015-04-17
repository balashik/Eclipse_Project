using UnityEngine;
using System.Collections;


public class networkFighter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	public OVRCameraRig myCam;
	public bool amIPilot;
	void Start(){
		if (photonView.isMine) {
			gameObject.GetComponent<fighterGuns>().enabled = true;
			gameObject.GetComponent<fighterMotor>().enabled = true;
			myCam.gameObject.SetActive(true);


		} else {
				}
	}
	void Update(){
		if (photonView.isMine) {
		
		} else {

			transform.position = Vector3.Lerp(transform.position,realPosition,0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation,realRotation,0.1f);
		}
	}


	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){

		if (stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		
		} else {
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
		}

	}
}
