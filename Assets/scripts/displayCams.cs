using UnityEngine;
using System.Collections;

public class displayCams : MonoBehaviour {

	
	Texture2D texture;
	public Material AppliedTo;
	
	//Turn this on to automatically set depth
	public bool autoDepth;
	
	void Start () {
		if(autoDepth && camera != Camera.main)
			camera.depth=Camera.main.depth+1;
		texture = new Texture2D(Mathf.RoundToInt(Screen.width), Mathf.RoundToInt(Screen.height), TextureFormat.ARGB32, false);
		
	}
	
	//Called after current camera has rendered
	void OnPostRender () {
			texture.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);
			texture.Apply ();
		
			AppliedTo.mainTexture = texture;
	}

}
