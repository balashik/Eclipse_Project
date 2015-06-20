using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
	public GameObject radCenterObject;
	public GameObject asteroidDots;
	GameObject[] asteroidsArray;
	List <GameObject> radarBlips;
	public GameObject centerCircle;
	public int radarDistance=50000;


	void Start(){
		radarBlips = new List<GameObject>();
		asteroidsArray = GameObject.FindGameObjectsWithTag ("Asteroid");
		if (asteroidsArray != null) {
			placeDots ();		
		} else {
			Debug.Log("no astroids in scene");
		}
	}
	void Update(){
		if (asteroidsArray != null) {
			updateDots ();
			asteroidDots.transform.localEulerAngles = transform.parent.localEulerAngles;
		}

	}



	//updates the position of the dots
	//consider changing this to ONE array which holds the list of ships, and checks if they are hostile/friendly, then 
	//draws them, no need to hold a list of blips, I think.
	public void updateDots()
	{
		for (int i = 0; i< asteroidsArray.Length; i++)
		{
			if(asteroidsArray[i]!=null){
				float x = asteroidsArray[i].transform.position.x - transform.parent.position.x;
				float y = asteroidsArray[i].transform.position.y - transform.parent.position.y;
				float z = asteroidsArray[i].transform.position.z - transform.parent.position.z;
				radarBlips[i].transform.localPosition = new Vector3(z,y,x) / radarDistance;
				if ((radCenterObject.transform.localPosition - radarBlips[i].transform.localPosition).sqrMagnitude < 0.3) {
					radarBlips[i].SetActive(true);
					drawLine(radarBlips[i]);
				}
				else{
					radarBlips[i].SetActive(false);
				}
			}
			else{
				Destroy(radarBlips[i]);
				Debug.Log("astroid was destroyed");
			}


		}
	}


	//creates and places dots on the radar sphere for the 1st time.
	public void placeDots()
	{
		foreach (GameObject a in asteroidsArray) 
		{
			float x = a.transform.position.x - transform.parent.position.x;
			float y = a.transform.position.y - transform.parent.position.y;
			float z = a.transform.position.z - transform.parent.position.z;

			GameObject newBlip = Instantiate(Resources.Load ("RadarGreenDot"), new Vector3(z,y,x) / radarDistance, Quaternion.identity)as GameObject;
			newBlip.transform.parent = asteroidDots.transform;
			newBlip.SetActive(false);
			radarBlips.Add(newBlip);

		}
	}


	void drawLine(GameObject dot){
		/*Vector3 dir = (centerCircle.transform.localPosition.y < dot.transform.localPosition.y) ? (-centerCircle.transform.up) : centerCircle.transform.up;

		RaycastHit hit;

		if(Physics.Raycast(dot.transform.position,dir, out hit))
		{


			if (hit.collider.name == "RadarCenterCircle"){
				dot.GetComponent<LineRenderer>().SetPosition(0,dot.transform.position);
				dot.GetComponent<LineRenderer>().SetPosition(1,hit.point);

			}

		}*/
		RaycastHit hit;
		if(Physics.Raycast(dot.transform.position,centerCircle.transform.up, out hit)){
			if(hit.collider.name=="RadarCenterCircle"){
				dot.GetComponent<LineRenderer>().SetPosition(0,dot.transform.position);
				dot.GetComponent<LineRenderer>().SetPosition(1,hit.point);
				dot.GetComponent<LineRenderer>().SetPosition(2,radCenterObject.transform.position);
				dot.GetComponent<LineRenderer>().SetColors(Color.green,Color.green);
				
			}
		}
		if(Physics.Raycast(dot.transform.position,-centerCircle.transform.up, out hit)){
			if(hit.collider.name=="RadarCenterCircle"){
				dot.GetComponent<LineRenderer>().SetPosition(0,dot.transform.position);
				dot.GetComponent<LineRenderer>().SetPosition(1,hit.point);
				dot.GetComponent<LineRenderer>().SetPosition(2,radCenterObject.transform.position);
				dot.GetComponent<LineRenderer>().SetColors(Color.red,Color.red);
			}
		}
	}
}

	

