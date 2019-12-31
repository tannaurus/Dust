using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Velocity_Controller : MonoBehaviour
{

	public float thrust = 0f;

	private int gear = 1;
	private int maxGear = 5;

	public Rigidbody engine;

	public void FixedUpdate() {
		ApplyTrust();
	}

	public void Update() {
		Transmission();
	}

	// Getters

	// Actors
	private void ApplyTrust() {
		engine.AddForce(transform.forward * thrust * 100f);
	}

	// Watchers
	private void Transmission() {
			if (Input.GetKeyDown(KeyCode.E) && gear < maxGear) {
				thrust++;
			}
			if (Input.GetKeyDown(KeyCode.Q) && gear > 0) {
				thrust--;
			}
	}


}
