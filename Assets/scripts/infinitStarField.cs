using UnityEngine;
using System.Collections;

public class infinitStarField : MonoBehaviour {
	Transform tx;
	ParticleSystem.Particle[] points;
	
	public int starsMax;
	public float starSize;
	public float starDistance;
	public float starClipDistance;
	// Use this for initialization
	void Start () {
	
		starsMax = 1000;
		starSize = 0.2f;
		starDistance = 100;
		starClipDistance = 1;
		tx = transform.parent;
	}

	void createStars(){
		points = new ParticleSystem.Particle[starsMax];
		
		for (int i=0; i<starsMax; i++) {
			points[i].position = Random.insideUnitSphere.normalized * starDistance + tx.position;
			points[i].color = new Color(1,1,1,1);
			points[i].size = starSize;
		}
	}
	// Update is called once per frame
	void Update () {
		if (points == null) {
			createStars();		
		}

		for (int i=0; i<starsMax; i++) {
			if((points[i].position - tx.position).sqrMagnitude > (starDistance*starDistance)){
				points[i].position = Random.insideUnitSphere * starDistance + tx.position;
			}
			if((points[i].position - tx.position).sqrMagnitude <= (starClipDistance*starClipDistance)){
				float percent = (points[i].position - tx.position).sqrMagnitude/starClipDistance;
				points[i].color = new Color(1,1,1, percent);
				points[i].size = percent*starSize;
			}
		}

		particleSystem.SetParticles (points, points.Length);
	
	}



}
