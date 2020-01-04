using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
	public GameObject[] logs;
	public GameObject[] discardOnDeath;
	public float health = 10f;
	public ParticleSystem particleSystem;

	private float maxHealth = 10f;
	private Collision lastCol;
	private bool beingDamaged = false;
	private bool dead = false;

	void Update()
	{
		if (beingDamaged) {
			health -= Time.deltaTime + GetDamageMultiplier();
		} else if (health != 0f) {
			health += Time.deltaTime;
		}

		health = Mathf.Clamp(health, 0f, maxHealth);
		if (health == 0f && !dead) {
			OnDeath();
		}
	}

	void OnCollisionEnter(Collision col) {
		lastCol = col;
		beingDamaged = true;
	}

	void OnCollisionExit(Collision col) {
		beingDamaged = false;
	}

	void OnDeath() {
		ReleaseLogs();
		DiscardObjects();
		TurnOffParticles();
		dead = true;
	}

	// Helpers
	float GetDamageMultiplier() {
		return lastCol.rigidbody.mass;
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
