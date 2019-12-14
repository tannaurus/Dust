using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Velocity_Controller : MonoBehaviour
{

	public float thrust = 0f;

	private int gear = 1;

	public FixedUpdate() {

	}

	public Update() {
		Transmission();
	}

	// Getters

	// Actors
	void AppleTrust() {
		
	}

	// Watchers
	void Transmission() {
			if (Input.GetKeyDown(KeyCode.E) && gear < maxGear) {
				thrust++;
			}
			if (Input.GetKeyDown(KeyCode.Q) && gear > 0) {
				thrust--;
			}
	}


}
