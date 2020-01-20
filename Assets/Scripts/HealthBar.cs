using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public GameObject healthBar;
	public float health;

	private Vector3 scaleRef;
	private float lastHealthChanged;
	private float lastKnownHealth;
	void Start()
	{
			scaleRef = healthBar.transform.localScale;
			healthBar.transform.localScale = new Vector3(1, scaleRef.y, scaleRef.z);
			lastKnownHealth = health;
			// Subtract some to indicate we haven't been hurt in some time.
			// This will ensure the bar is hidden on run time.
			lastHealthChanged = Time.time - 5f;
	}

	void Update()
	{
			OnHealthChange();
			RevealAndHideBar();
			UpdateWidth();
	}

	// Updaters
	void UpdateWidth() {
		float width = Mathf.Clamp(health / 100, 0, 1);
		healthBar.transform.localScale = new Vector3(width, scaleRef.y, scaleRef.z);
	}

	void OnHealthChange() {
		if (health != lastKnownHealth) {
			lastKnownHealth = health;
			lastHealthChanged = Time.time;
		}
	}

	/*
	* TO-DO:
	* - RevealAndHideBar should fade the game object in and out using the animator.
	*/
	void RevealAndHideBar() {
		float timeSinceHealthChanged = Time.time - lastHealthChanged;
		if (
			healthBar.active && 
			timeSinceHealthChanged > 3f
		) {
			healthBar.SetActive(false);	
		}
		if (!healthBar.active &&
			timeSinceHealthChanged < 3f
		) {
			healthBar.SetActive(true);
		}
	}
}
