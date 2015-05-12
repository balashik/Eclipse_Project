using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu currentMenu;
	bool isMulti;
	
	void Start(){
		currentMenu.setIsOpen (true);
	}


	public void pickSinglePlayer(){
		Application.LoadLevel ("singlePlayer");
		
	}

	public void pickMultiPlayer(){
		Application.LoadLevel ("multiPlayer");
		
	}

	public void showMenu(Menu menu){
		
		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	
	}
	/*public void pickSinglePlayer(Menu menu){

		isMulti = false;
		runLevelWithSettings ();
		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	}

	public void pickMultiPlayer(Menu menu){
		isMulti = true;
		runLevelWithSettings ();
		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);


		runLevelWithSettings ();
	
	}

	public void pickGunner(Menu menu){

		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	




		runLevelWithSettings ();

	}


	public void pickPilot(Menu menu){
	
		currentMenu.setIsOpen (false);
		currentMenu = menu;
		currentMenu.setIsOpen (true);
	
	}
	
	public void runLevelWithSettings(){
		if (isMulti) {
				Application.LoadLevel ("multiPlayer");
		}
		else{
				Application.LoadLevel ("singlePlayer");
		}

	}*/



	public void exitMenu(){
		Application.Quit ();
	}
}
