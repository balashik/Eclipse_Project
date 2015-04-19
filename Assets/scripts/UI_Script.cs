//Project EcliPse - Shenkar final project 2015.
//Gal Shalit, Yaniv Levi, David Faizulaev & Avishag Zehavi
using UnityEngine;
using UnityEngine.UI; // we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.EventSystems; // we need this namespace in order to access UI events within our script
 
public class UI_Script : MonoBehaviour 
{
	//Main menu elements
	public Canvas CanvasMain_Menu;
	public Button ButtonSINGLEPLAYER;
	public Button ButtonMULTIPLAYER;
	public Button ButtonSettings;
	public Button ButtonExit;

	//Sub menu elements
	public Canvas Role_Menu;
	public Button ButtonPILOT;
	public Button ButtonGUNNER;
	private bool SingleMode=true;

	//Loading Screen
	public Canvas loadingScreen;



	//Settings section elements
	public Canvas Canvas_Sett;

	//Allpurpose Back button
	public Button ButtonBACK;
	

    void Start ()
    {
		Debug.Log ("Load main menu");

		//Initialize Main menu elements
		CanvasMain_Menu = CanvasMain_Menu.GetComponent<Canvas>();
		ButtonSINGLEPLAYER = ButtonSINGLEPLAYER.GetComponent<Button> ();
		ButtonMULTIPLAYER = ButtonMULTIPLAYER.GetComponent<Button> ();
		ButtonSettings = ButtonSettings.GetComponent<Button> ();
		ButtonExit = ButtonExit.GetComponent<Button>();

		//Initialize Sub menu elements
		Role_Menu = Role_Menu.GetComponent<Canvas>();
		Role_Menu.enabled = false;
		ButtonPILOT = ButtonPILOT.GetComponent<Button> ();
		ButtonGUNNER = ButtonGUNNER.GetComponent<Button> ();

		//Initialize Settings elements
		Canvas_Sett = Canvas_Sett.GetComponent<Canvas>();
		Canvas_Sett.enabled = false;

		//Initialize LoadingScreen elements
		loadingScreen.enabled = false;
		//Allpurpose Back button
		ButtonBACK = ButtonBACK.GetComponent<Button> ();



		Debug.Log ("Main menu loaded");
    }
 
	public void SettingsPressed ()
	{
		Debug.Log ("SettingsPressed");
		CanvasMain_Menu.enabled = false; //disable the main menu
		Canvas_Sett.enabled = true;
	}

    public void SinglePlayerPress() //this function will be used on our Exit button
    {
		Debug.Log ("SinglePlayerPress");
		CanvasMain_Menu.enabled = false; //disable the main menu
		Role_Menu.enabled = true; //enable the Select Role menu

		//Enable sub menu buttons.
		ButtonPILOT.enabled = true;
		ButtonGUNNER.enabled = true;
		ButtonBACK.enabled = true;
		Debug.Log ("set play mode single");
		SingleMode=true;
    }
 
	public void MultiPlayerPress() //this function will be used on our Exit button
	{
		Debug.Log ("MultiPlayerPress");
		CanvasMain_Menu.enabled = false; //disable the main menu
		Role_Menu.enabled = true; //enable the Select Role menu
		
		//Enable sub menu buttons.
		ButtonPILOT.enabled = true;
		ButtonGUNNER.enabled = true;
		ButtonBACK.enabled = true;
		Debug.Log ("set play mode multi");
		SingleMode=false;
	}

	public void GunnerPress() //this function will be used on our Gunner button
	{
		Debug.Log ("GunnerPress");
		CanvasMain_Menu.enabled = false;
		Role_Menu.enabled = false;
		//After role is selected the NetworkManager object script is called and user is connected to Photon server

		Debug.Log ("checking play mode");
		if (SingleMode == true) {
			StartSingePlayerLevel ();
				}
		else {
			StartMultiPlayerLevel ();
		}
	}

	public void PilotPress() //this function will be used on our Pilot button
	{
		Debug.Log ("PilotPress");
		CanvasMain_Menu.enabled = false;
		Role_Menu.enabled = false;
		//After role is selected the NetworkManager object script is called and user is connected to Photon server

		Debug.Log ("checking play mode");
		if (SingleMode == true) {
			StartSingePlayerLevel ();
		}
		else {
			StartMultiPlayerLevel ();
		}
	}

	//Go back to main menu
	public void PressBack()
	{
		Debug.Log ("PressBack");
		//Disable sub menu & settings
		Role_Menu.enabled = false;
		Canvas_Sett.enabled = false;

		//Re-enable main menu
		CanvasMain_Menu.enabled = true;
		Debug.Log ("set play mode single");
		SingleMode=true;
	}
	
    void StartSingePlayerLevel () //this function will be used on our Play button
    {
		Debug.Log ("loading singleplayer mode");
	 	//Application.LoadLevel ("singlePlayer"); //this will load our first level from our build settings. "1" is the second scene in our game 
		StartCoroutine (loadingScreenDisplay("singlePlayer"));
    }
 
	void StartMultiPlayerLevel () //this function will be used on our Play button
	{
		Debug.Log ("loading multiplayer mode");
		//Application.LoadLevel ("MultiPlayer"); //this will load our first level from our build settings. "1" is the second scene in our game 
		StartCoroutine (loadingScreenDisplay("multiPlayer"));
	}

    public void QuitGamePress () //This function will be used on our "Yes" button in our Quit menu       
    {
		Debug.Log ("QuitGamePress");
  		Application.Quit(); //this will quit our game. Note this will only work after building the game    
    }

	public void SinglePButtonResize ()
	{
		ButtonSINGLEPLAYER.image.rectTransform.sizeDelta = new Vector2( 165, 40);
	}

	public void SinglePButtonSizeBack ()
	{
		ButtonSINGLEPLAYER.image.rectTransform.sizeDelta = new Vector2( 160, 30);
	}

	public void MultiPButtonResize ()
	{
		ButtonMULTIPLAYER.image.rectTransform.sizeDelta = new Vector2( 165, 40);
	}

	public void MultiPButtonSizeBack ()
	{
		ButtonMULTIPLAYER.image.rectTransform.sizeDelta = new Vector2( 160, 30);
	}

	public void SettingsButtonResize ()
	{
		ButtonSettings.image.rectTransform.sizeDelta = new Vector2( 165, 40);
	}
	
	public void SettingsPButtonSizeBack ()
	{
		ButtonSettings.image.rectTransform.sizeDelta = new Vector2( 160, 30);
	}

	public void ExitButtonResize ()
	{
		ButtonExit.image.rectTransform.sizeDelta = new Vector2( 165, 40);
	}
	
	public void ExitPButtonSizeBack ()
	{
		ButtonExit.image.rectTransform.sizeDelta = new Vector2( 160, 30);
	}



	IEnumerator loadingScreenDisplay(string levelName){
		loadingScreen.enabled = true;




		AsyncOperation async = Application.LoadLevelAsync (levelName);
		while (!async.isDone) {

			yield return null;	
		}

	}
}