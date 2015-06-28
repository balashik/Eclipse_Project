using UnityEngine;
using System.Collections;

public class asteroidsCol : MonoBehaviour {

	void OnCollisionEnter(Collision collide)
	{    
		if(collide.gameObject.tag == "Asteroid")        
		{
			gameObject.GetComponent<Health>().addDamage(-100);
			
		}        
	}

}


