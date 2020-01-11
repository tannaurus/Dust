using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{	
		public float horizontalSensitivity = 1.5f;
    public float verticalSensitivity = 1.2f;
    private Player_Controller playerController;
		private Item itemInfo;

    public void Start()
    {
    	playerController = GetComponentInParent<Player_Controller>();
			itemInfo = GetComponent<Item>();
    }

		public void Update() {
			// GliderController();
		}

		private void GliderController() {
			HandlePlayerControllerOverrideStatus();
			HandleVerticalDirectionAndSpeed();
			// Handle vertical direction change clamped to angle, apply speed based on vertical angle
			// Handle horizontal direction change clamped to angle
			// Always drop height just a little
		}

		// Helpers
		private void HandleVerticalDirectionAndSpeed() {
			float xAxis = GetClampedVerticalAxis();
			float zAxis = GetClampedHorizontalAxis();
			transform.parent.parent.Rotate(xAxis, 0.0f, zAxis);
			float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.parent.parent.position);
			if (terrainHeightWhereWeAre - transform.parent.parent.position.y < 0) {
					Debug.Log("Ground!");
			}
		}
		private void HandlePlayerControllerOverrideStatus() {
			if (!playerController.forceOverride && itemInfo.equipped) {
				playerController.forceOverride = true;
			} else if (playerController.forceOverride && !itemInfo.equipped) {
				playerController.forceOverride = false;
			}
		}

		private float GetClampedVerticalAxis() {
			float verticalAxis = Input.GetAxis("Vertical") * verticalSensitivity;
			float parentX = transform.parent.parent.eulerAngles.x;
			// Arbitrary clamps because this value passes the 0 mark and jumps straight to 360
			if (parentX > 50 && parentX < 80) {
				verticalAxis = -1f;
			} else if (parentX > 300 && parentX < 345) {
				verticalAxis = 1f;
			}
			return verticalAxis;
		}

		private float GetClampedHorizontalAxis() {
			float horizontalAxis = Input.GetAxis("Horizontal") * horizontalSensitivity * -1;
			float parentZ = transform.parent.parent.eulerAngles.z;
			// Arbitrary clamps because this value passes the 0 mark and jumps straight to 360
			if (parentZ > 45 && parentZ < 90) {
				horizontalAxis = -1f;
			} 
			else if (parentZ < 315 && parentZ > 250) {
				horizontalAxis = 1f;
			}
			return horizontalAxis;
		}

}
