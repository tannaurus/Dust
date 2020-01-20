using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
	public GameObject[] logs;
	public GameObject[] discardOnDeath;
	public float health = 10f;
	public ParticleSystem particleSystem;

	private float maxHealth;
	private Collider lastCol;
	private bool beingDamaged = false;
	private bool dead = false;
	private Ambient_Damage ambientDamage;

	void Start() {
		ambientDamage = GetComponent<Ambient_Damage>();
		maxHealth = health;
	}

	void Update()
	{
		if (beingDamaged) {
			health -= GetDamageMultiplier() * Time.deltaTime;
		} else if (health != 0f) {
			health += 1 * Time.deltaTime;
		}

		health = Mathf.Clamp(health, 0f, maxHealth);
		if (health == 0f && !dead) {
			OnDeath();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Terrain" || col.gameObject.tag == "Campfire_Log") {
			return;
		}
		lastCol = col;
		beingDamaged = true;
	}

	void OnTriggerExit(Collider col) {
		beingDamaged = false;
	}

	void OnDeath() {
		Debug.Log("Logs died!");
		ReleaseLogs();
		DiscardObjects();
		TurnOffParticles();
		dead = true;
		ambientDamage.stopAmbientDamage = true;
	}

	// Helpers
	float GetDamageMultiplier() {
		return lastCol.GetComponent<Rigidbody>().mass;
	}

	void ReleaseLogs() {
		for (int i = 0; i < logs.Length; i++) {
			logs[i].GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	void DiscardObjects() {
		for (int i = 0; i < discardOnDeath.Length; i++) {
			Destroy(discardOnDeath[i]);
		}
	}

	void TurnOffParticles() {
		particleSystem.Stop();
	}
}
