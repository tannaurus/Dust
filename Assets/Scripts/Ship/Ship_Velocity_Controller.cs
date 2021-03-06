﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Velocity_Controller : MonoBehaviour
{
    public float speed = 90f;
		public float speedMultiplier = 50f;
		private float maxSpeed = 100f;
		public int gear = 0;
		private int maxGear = 3;

    // Update is called once per frame
    void Update()
    {
				// AmbientSpeedController();
				PlayerSpeedController();
				Transmission();
				UpdateShipPosition();
		}

		void AmbientSpeedController() {
			// Prevent the ship from moving backwards too fast.
			float clampedForward = Mathf.Clamp(transform.forward.y, -0.8f, 0.5f);
			float speedOffset = clampedForward * Time.deltaTime;

			speed -= speedOffset;
		}

		void PlayerSpeedController() {
			float gearMultiplier = GetGearMultiplier();
			speedMultiplier = gearMultiplier;
		}

		void UpdateShipPosition() {
			transform.position += transform.forward * Time.deltaTime * (speed * speedMultiplier);
		}

    void Transmission() {
        if (Input.GetKeyDown(KeyCode.E) && gear < maxGear) {
					gear++;
				}
				if (Input.GetKeyDown(KeyCode.Q) && gear > 0) {
					gear--;
				}
				if (Input.GetKeyDown(KeyCode.R) && gear != 0) {
					gear = 0;
				}
    }

		float GetGearMultiplier() {
			switch(gear) {
				case 0:
					return 0f;
				case 1:
					return 4f;
				case 2:
					return 8f;
				case 3:
					return 15f;
				default:
					return 0f;
			}
		}
}