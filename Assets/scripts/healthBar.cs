using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {

	
	public void addHealth(float healthAmount ){
		gameObject.GetComponent<Image> ().fillAmount += healthAmount;
	}
	public float getHealth(){
		return gameObject.GetComponent<Image> ().fillAmount;

	}
}
