using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu currentMenu;

	void Start(){
		showMenu (currentMenu);
	}
	/*public void showMenu(Menu menu){
		if(currentMenu!=null){
			currentMenu.setIsOpen(false);

		}
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	}*/

	public void showMenu(Menu menu){
		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	}

	public void LoadLevel(string levelName){
		Application.LoadLevel (levelName);
	}


	public void exitMenu(){
		Application.Quit ();
	}
}
