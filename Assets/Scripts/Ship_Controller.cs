using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Controller : MonoBehaviour
{
    public float speed = 90f;
		public float speedMultiplier = 50f;
		private float maxSpeed = 100f;
		public int gear = 0;
		private int maxGear = 3;
    public float horizontalSensitivity = 3f;
    public float verticalSensitivity = 2f;

    // Update is called once per frame
    void Update()
    {
				AmbientSpeedController();
				PlayerSpeedController();
				Transmission();
				UpdateShipPosition();
		}

		void AmbientSpeedController() {
			// Prevent the ship from moving backwards too fast.
			float clampedForward = Mathf.Clamp(transform.forward.y, -0.8f, 0.5f);
			float speedOffset = clampedForward * Time.deltaTime;

			Debug.Log(speedOffset);
			speed -= speedOffset;

			transform.Rotate(Input.GetAxis("Vertical") * verticalSensitivity, 0.0f, Input.GetAxis("Horizontal") * horizontalSensitivity * -1);
			float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
			if(terrainHeightWhereWeAre > transform.position.y) {
					Debug.Log("Ground!");
			}
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
