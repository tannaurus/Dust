using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
	public float speedMultiplier = 1f; 
	public float mouseSensitivityMultiplier = 1.2f;
	public float jumpForce = 5f;
	private Animator animator;
	private Rigidbody rigidbody;
	void Start()
	{
			animator = GetComponent<Animator>();
			rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		MovePlayer();

		// RotatePlayer();

		Jump();
	}

	// Actions
	void MovePlayer() {
		Vector3 displacement = transform.position;
		if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S)) {
			animator.SetTrigger("Walk");
			if (Input.GetKey(KeyCode.W)) {
				displacement += transform.forward;
			}
			if (Input.GetKey(KeyCode.S)) {
				displacement -= transform.forward;
			}
		} else {
			animator.SetTrigger("Idle");
		}
		transform.position = Vector3.Lerp(transform.position, displacement, speedMultiplier * Time.deltaTime);
	}

	// void RotatePlayer() {
	// 	Vector2 playerPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position); 
	// 	Vector2 mousePositionOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
	// 	float angleBetweenPositions = AngleBetweenTwoPoints(playerPositionOnScreen, mousePositionOnScreen);
	// 	transform.rotation =  Quaternion.Euler (new Vector3(0f, -angleBetweenPositions * mouseSensitivityMultiplier, 0f));
	// }

	void Jump() {
		if (IsGrounded() & Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.AddForce(transform.up * jumpForce);
		}
	}
	
	// Helpers
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
	}
}
