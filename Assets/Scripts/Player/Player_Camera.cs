using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
	public Inventory_Input_Listener inventoryInputListener;

	// void Update()
	// {
	//     CameraController();
	// }

	// void CameraController() {
	//     Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
	//     float bias = 0.9f;
	//     Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f-bias);
	//     Camera.main.transform.LookAt(transform.position + transform.forward);
	// }
	public float mouseSpeed = 3;

	private void Update()
	{
		if (inventoryInputListener.inventoryOpen) {
				return;
		}
		float X = Input.GetAxis("Mouse X") * mouseSpeed;
		float Y = Input.GetAxis("Mouse Y") * mouseSpeed;

		transform.Rotate(0, X, 0);

		if (Camera.main.transform.eulerAngles.x + (-Y) < 80 & Camera.main.transform.eulerAngles.x + (-Y) < 280 & Camera.main.transform.eulerAngles.x + (-Y) > 0) {
				Camera.main.transform.RotateAround(transform.position, Camera.main.transform.right, -Y);
		}
	}
}
