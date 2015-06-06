using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
	public GameObject green;
	public GameObject[] asteroidsArray;
	List <GameObject> blackDotsList;



	// Use this for initialization
	void Start () {
		blackDotsList = new List<GameObject>();
		getCurrentAsetroidesArray ();
		placeDots ();
	}
	
	// Update is called once per frame
	void Update () {
		updateDots ();
	}





	void getCurrentAsetroidesArray(){
		asteroidsArray = GameObject.FindGameObjectsWithTag ("Asteroid");
	}


	void placeDots(){
		foreach (GameObject a in asteroidsArray) {
			GameObject temp = Instantiate(Resources.Load("RadarGreenDot"),Vector3.zero,a.transform.rotation)as GameObject;
			temp.transform.parent = transform;
			temp.transform.localPosition= new Vector3((transform.parent.position.x-a.transform.position.x)/3000,(transform.parent.position.y-a.transform.position.y)/3000,(transform.parent.position.z-a.transform.position.z)/3000);
			temp.SetActive(false);

			/*if((Mathf.Abs(temp.transform.localPosition.x)<0.5)&&(Mathf.Abs(temp.transform.localPosition.y)<0.5)&&(Mathf.Abs(temp.transform.localPosition.z)<0.5)){
				temp.SetActive(true);
			}*/
			blackDotsList.Add (temp);
		}

	}

	void updateDots(){
		for(int i=0;i<asteroidsArray.Length;i++){
			if(asteroidsArray[i]==null){
				Debug.Log("destroyed black dot");
				Destroy(blackDotsList[i]);

			}
			blackDotsList[i].transform.localPosition = new Vector3((transform.parent.position.x-asteroidsArray[i].transform.position.x)/3000,(transform.parent.position.y-asteroidsArray[i].transform.position.y)/3000,(transform.parent.position.z-asteroidsArray[i].transform.position.z)/3000); 
			if((green.transform.localPosition-blackDotsList[i].transform.localPosition).sqrMagnitude<0.3){
				blackDotsList[i].SetActive(true);
			}
			else{
				blackDotsList[i].SetActive(false);
			}
		}
	}

	void FixedUpdate(){


	}
}
