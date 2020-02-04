using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Axis_Controller : MonoBehaviour
{
    public float horizontalSensitivity = 3f;
    public float verticalSensitivity = 2f;

    void Update()
    {
				AxisController();
		}

		void AxisController() {
			transform.Rotate(Input.GetAxis("Vertical") * verticalSensitivity, 0.0f, Input.GetAxis("Horizontal") * horizontalSensitivity * -1);
			float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
			if(terrainHeightWhereWeAre > transform.position.y) {
					Debug.Log("Ground!");
			}
		}
}