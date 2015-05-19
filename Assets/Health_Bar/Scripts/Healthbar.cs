using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteDatabase))]
public class Healthbar : MonoBehaviour {

	//GABROMEDIA@GMAIL.COM

	//Health works on a 1000 (integer) scale so it gives a wider range when it comes to decreasing/increasing the value incrementally
	//Health percentage is stored in healthNormalized integer.

	Image frame;
	Image bar;
	int health = 1000;
	int healthNormalized;
	GameObject player;

	//This public boolean stores the main data whether player's dead or alive.
	[HideInInspector]
	public bool alive = true;

	//Defines when to display the message "Critical" and at what value the healthbar should turn red from green or yellow
	public float displayCritical;
	public int displayRedBar;
	public int displayYellowBar;

	//If checked true in the inspector, it will display the health value above the healthbar.
	public bool showHealthValue;

	//Position of the text above the bar
	Rect healthText;

	//Reference to SpriteDatabase
	SpriteDatabase sd;

	//Developer can choose which theme they want to use
	public Themes chosenTheme;

	//Identifier of the chosen theme in the Theme List<>
	int myTheme;

	//For demo purposes, store player's initial transform (so later it can be respawned there)
	Vector3 startPos;

	//On Start, assign SpriteDatabse class to 'sd'. (Note: That class can never be missing due to the dependency system)
	//It then runs Debugger() (find it below.) It checks whether the required sprites are assigned in the inspector, etc.
	//Then, it builds hierarchy for GUI (find below)
	void Start(){
		sd = GetComponent<SpriteDatabase>();
	//	Debugger();
		BuildHierarchy();
//		startPos = player.transform.position;
	}


	//Converts health integer to float value and updates it every frame.
	//Keeps the GUI bar (image) fill amount value synchronized with the health value.
	//Note: healthNormalized cuts the number so that it's on a 100 scale like in every game (it's basically the percentage)
	void FixedUpdate(){
		if (alive) {
			if (healthNormalized <= 0) {
				alive = false;
				die();	
			}
			healthNormalized = health/10;
			//Converts health value to a float (range 0-1) so it can be used for image.fillamount
			float healthValue = health * 0.001f;
			healthValue = Mathf.Clamp(healthValue, 0, 1);

			//Checks if it's time to turn the bar color to red (replace the sprite basically)
			CheckForBarColor();

			bar.fillAmount = healthValue;
		}
		//Check every frame whether player has fallen off the map
	//	PlayerFallsDown();
	}

	//Die if player falls off the ground
	//void PlayerFallsDown(){
	//	if (player.transform.position.y < -3.0F) {
	//		health = 0;
	//	}
	//}

	//Called by every object affecting player's health.
	//Class that calls it: ApplyDamage
	//See that for more info on how to use it!
	public void ModifyHealth(int amount) {
		if (alive)
			health = health - amount;

		health = Mathf.Clamp(health, 0, 1000);
	}

	//Modify this to change the way of dieing (this just for the demo scene (respawn player at starting location after 2 seconds)
	//Find IENumerator at the very bottom of the code.
	void die(){
		StartCoroutine(Resurrection());
	}

	//Runs every frame in FixedUpdate. Soon as player's health goes below 30%, it changes texture to the red one.
	//If you don't need it, simply take out the function from FixedUpdate, or modify value to get a different result.
	//It only attempts to change the bar color, if it's not changed already (optimization)
	void CheckForBarColor(){
		if (healthNormalized > displayYellowBar)
			bar.sprite = sd.sprites[myTheme].GreenBar;
		else if (healthNormalized > displayRedBar)
			bar.sprite = sd.sprites[myTheme].YellowBar;
		else
			bar.sprite = sd.sprites[myTheme].RedBar;
	}	

	//If developer ticked "Show Health Value" in inspector, it will be displayed on the screen
	void OnGUI (){
		GUI.skin = sd.guiSkin;

		if (showHealthValue) {
			GUI.Label(new Rect(10, 10, 190, 20), "Health: " + healthNormalized.ToString());
		}

		if (healthNormalized <= displayCritical && alive && showHealthValue) {
			GUI.Label(new Rect(10, 70, 190, 20), "Critical");
		}

		if (!alive) {
			GUI.Label(new Rect(10, 70, 190, 20), "YOU'RE DEAD!");
		}
	}




	//Below this line the script is basically loading in the chosen theme based on the selection from the inspector
	//Selection is based on 'Theme' enum
	//-------------------------------------------------------------------------------------------------------------


	void BuildHierarchy(){

		//Anchor is in upper left corner of the canvas 
		Vector2 anchors = new Vector2(0, 1);

		//Create a canvas
		//---------------------------------------------------------------------------------------
		GameObject canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
		canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		//---------------------------------------------------------------------------------------
		//Create healthbar (green) itself
		//---------------------------------------------------------------------------------------
		GameObject barObject = new GameObject("Healthbar", typeof(CanvasRenderer), typeof(Image));
		RectTransform barRect = barObject.GetComponent<RectTransform>();
		//Position anchors and parent it to canvas
		barRect.anchorMax = anchors;
		barRect.anchorMin = anchors;
		barObject.transform.SetParent(canvasObject.transform);
		//Set image type to filled
		bar = barObject.GetComponent<Image>();
		bar.fillMethod = Image.FillMethod.Horizontal;
		bar.type = Image.Type.Filled;
		//----------------------------------------------------------------------------------------
		//Create healthbar frame
		//----------------------------------------------------------------------------------------
		GameObject barFrameObject = new GameObject("Healthbar_frame", typeof(CanvasRenderer), typeof(Image));
		RectTransform barFrameRect = barFrameObject.GetComponent<RectTransform>();
		frame = barFrameObject.GetComponent<Image>();
		//Anchors and parenting to canvas
		barFrameRect.anchorMax = anchors;
		barFrameRect.anchorMin = anchors;
		barFrameRect.transform.SetParent(canvasObject.transform);
		//----------------------------------------------------------------------------------------
		//Check sprite dimensions and position them based on their sizes
		//----------------------------------------------------------------------------------------
		float textureHeight = sd.sprites[myTheme].GreenBar.bounds.size.y *100;
		float textureWidth = sd.sprites[myTheme].GreenBar.bounds.size.x*100;
		float yPos = Screen.height - textureHeight;
		float xPos = Screen.width - (Screen.width-(textureWidth/1.5f));

		Vector3 healthBarPosition = new Vector3(xPos, yPos, barObject.transform.position.z);

		barObject.transform.position = healthBarPosition;
		barFrameObject.transform.position = healthBarPosition;

		//Assign proper frame sprite
		frame.sprite = sd.sprites[myTheme].HealthBar_Frame;

		//Scale it
		Vector2 frameDimensions = new Vector2(frame.sprite.bounds.size.x, frame.sprite.bounds.size.y);
		Vector3 frameScale = new Vector3(frameDimensions.x, frameDimensions.y, 0.1F);
		barFrameRect.transform.localScale = frameScale;

		//Assign proper bar sprite
		bar.sprite = sd.sprites[myTheme].GreenBar;

		//Scale it
		Vector2 barDimensions = new Vector2(bar.sprite.bounds.size.x, bar.sprite.bounds.size.y);
		Vector3 barScale = new Vector3(barDimensions.x, barDimensions.y, 0.1F);
		barRect.transform.localScale = barScale;


		//Set healthNormalized
		healthNormalized = health/10;
	}

	//Returns an integer, the List index of where the theme can be found in SpriteDatabase.
	//If there's no match (eg. SpriteDatabase List isn't assigned in the inspector, it will return the first index (0)
	int FindThemeIndex(Themes t){
		for (int i = 0; i < sd.sprites.Count; i++) {
			if (sd.sprites[i].theme.theme == chosenTheme.theme)
				return i;
		}
		return 0;
	}

	//Check if 'Player' tag is in scene, and also if any of the 3 required sprites for the chosen theme are assigned
	//in the inspector at their designated fields. (frame, green and red bar)
	void Debugger(){
		myTheme = FindThemeIndex(chosenTheme);
		player = GameObject.FindGameObjectWithTag("Player");
	
		Sprite frame = sd.sprites[myTheme].HealthBar_Frame;
		Sprite green = sd.sprites[myTheme].GreenBar;
		Sprite red = sd.sprites[myTheme].RedBar;

		if (player == null) {
			Debug.LogError("No 'Player' tag in scene for Healthbar class!");
			Debug.Break();
		}
		if (sd == null) {
			Debug.LogError("SpriteDatabase class missing!");
			return;
		}

		if (frame == null || green == null || red == null) {
			Debug.LogError("Some or all sprites are not assigned for chosen theme in the inspector!");
			Debug.Break();
		}
	}

	//This is only for the demo - to bring player back to life after 2 seconds and respawn it back to the start position
	IEnumerator Resurrection(){
		yield return new WaitForSeconds(2.0F);
		player.transform.position = startPos;
		alive = true;
		health = 1000;

	}


	//void InstantiateHealthBar()
	//{
//		Instantiate (Healthbar);
//	}


}
