using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LoginManager : MonoBehaviour {
	
	public Menu currentMenu;
	public GameObject myCanvas;
	public void onConnectPressed(Menu menu){ //connect button action



		//login here



		//this line responsible on the menu swich right after the player was connected
		myCanvas.GetComponent<MenuManager> ().showMenu (currentMenu);
		
	}
}
