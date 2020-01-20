using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Damage : MonoBehaviour
{
	public float damagePerSecond = 3f;
	// Set from external scripts
	public bool stopAmbientDamage = false;

	private HealthSystem healthSystem;
	private bool giveDamage;
	private float lastDamaged;
	void Start()
	{
		lastDamaged = Time.time;  
	}

	void OnTriggerEnter(Collider col)
	{
		healthSystem = col.gameObject.GetComponent<HealthSystem>();
		if (!healthSystem) {
			return;
		}
		giveDamage = true;
	}

	void OnTriggerExit(Collider col) {
		healthSystem = col.gameObject.GetComponent<HealthSystem>();
		if (!healthSystem) {
			return;
		}
		giveDamage = false;
	}

	void Update() {
		if (!giveDamage || stopAmbientDamage) {
			return;
		}
		float timeSinceDamageGiven = Time.time - lastDamaged;
		if (timeSinceDamageGiven > 1f) {
			healthSystem.ApplyDamage(damagePerSecond);
			lastDamaged = Time.time;
		}
	}
}
