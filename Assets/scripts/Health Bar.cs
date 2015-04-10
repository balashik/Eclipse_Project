using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour 
{
	public float speed;
	public RectTransform healthTransform;
	float cachedY;
	float minXValue;
	float maxXValue;
	float currentHealth;
	float minHealth;
	
	private int CurrentHealth
	{
		get { return CurrentHealth;}
		set {
			CurrentHealth = value;
			HandleHealth();
			}
	}
	
	
	
	public float maxHealth;
	public Text healthText;
	public Image visualHealth;
	public float coolDown;
	private bool onCD;
	
	
 
	//Use this fir initialize
	void Start()
	{
	cachedY = healthTransform.position.y;
	maxXValue = healthTransform.position.x;
	minXValue = healthTransform.position.x - healthTransform.rect.width;
	currentHealth = maxHealth;
	onCD = false;
	}
 
	//update is called one per frame
	void Update()
	{

	}
	


 
 
 IEnumerator CoolDownDmg()
 {
	onCD = true;
	yield return new WaitForSeconds(coolDown);
	onCD = false;
 }
 
 
 
 
 
 
 
 
	private void HandleHealth()
 {
	healthText.text = "Health;" + currentHealth;
	
	//float currentXValue = MapValues(currentHealth,0, maxHealth, minHealth, minXValue , maxXValue);
	float currentXValue = MapValues(currentHealth,0, maxHealth, minXValue , maxXValue);
	healthTransform.position = new Vector3(currentXValue, cachedY);
	
	if(currentHealth > maxHealth/2)
		{
			visualHealth.color = new Color32((byte)MapValues(currentHealth,maxHealth/2,maxHealth, 255,0), 255,0,255);
		}
		else
		{
			visualHealth.color = new Color32(255,(byte)MapValues(currentHealth,0,maxHealth/2,0,255),0,255);
		}
	
 }
 
 
 
 
 
 
 
 
void OnTriggerStay(Collider other)
{
	if (other.name == "Damage")
	{
		if(!onCD && currentHealth > 0)
		{
			StartCoroutine(CoolDownDmg());
			CurrentHealth -= 1;
		}
	}
	if(other.name == "Health")
	{
		if(!onCD && currentHealth < maxHealth)
		{
			StartCoroutine(CoolDownDmg());
			CurrentHealth += 1;
		}
	}
}

private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
{
	return(x-inMin)*(outMax - outMin) / (inMax - inMin) + outMin;
}




}