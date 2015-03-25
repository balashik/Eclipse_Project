using UnityEngine;
using System.Collections;

public class thrusters : MonoBehaviour {

	public float thrusterForce = 10000;

	private Light _cacheLight;
	private ParticleSystem _cacheParticleSystem;
	private bool _isActive = false;	

	public void StartThruster() {
		_isActive = true; 
	}

	public void StopThruster() {
		_isActive = false; 
	}

	// Use this for initialization
	void Start () {
	
		_cacheLight = transform.GetComponent<Light>().light;
		if (_cacheLight == null) {
			Debug.LogError("Thruster prefab has lost its child light. Recreate the thruster using the original prefab.");
		}
		_cacheParticleSystem = particleSystem;
		if (_cacheParticleSystem == null) {
			Debug.LogError("Thruster has no particle system. Recreate the thruster using the original prefab.");
		}


	}
	
	// Update is called once per frame
	void Update () {
	
		if (_cacheLight != null) {
			// Set the intensity based on the number of particles
			_cacheLight.intensity = _cacheParticleSystem.particleCount / 20;
		}
		if (_isActive) {
			if (_cacheParticleSystem != null) {	
				_cacheParticleSystem.enableEmission = true;
			}
		} else {
			if (_cacheParticleSystem != null) {				
				// Stop emission of thruster particles
				_cacheParticleSystem.enableEmission = false;				
			}
		}
		
	}
}
