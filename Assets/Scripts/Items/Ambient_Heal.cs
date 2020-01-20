using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Heal : MonoBehaviour
{
	public float healthPerSecond = 1f;
	private HealthSystem healthSystem;
	private bool heal;
	private float lastHealed;

	void Start() {
		lastHealed = Time.time - 5f;
	}

	void Update()
	{
		if (!heal) {
			return;
		}
		float timeSinceLastHeal = Time.time - lastHealed;
		if (timeSinceLastHeal > 1f) {
			healthSystem.ApplyDamage(-healthPerSecond);
			lastHealed = Time.time;
		}
	}

	void OnTriggerEnter(Collider col) {
		HealthSystem colHealthSystem = col.gameObject.GetComponent<HealthSystem>();
		if (!colHealthSystem) {
			return; 
		}
		healthSystem = colHealthSystem;
		heal = true;
	}

	void OnTriggerExit(Collider col) {
		HealthSystem colHealthSystem = col.gameObject.GetComponent<HealthSystem>();
		if (!colHealthSystem) {
			return;
		}
		heal = false;
	}
}
