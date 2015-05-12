using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LoginManager : MonoBehaviour {
	
	public Menu currentMenu;
	public GameObject myCanvas;
	public void onConnectPressed(Menu menu){
		myCanvas.GetComponent<MenuManager> ().showMenu (currentMenu);
		
	}
}
