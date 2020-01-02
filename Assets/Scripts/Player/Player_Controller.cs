using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
	public float speedMultiplier = 1f; 
	public float mouseSensitivityMultiplier = 1.2f;
	public float jumpForce = 5f;
	public GameObject playerModel;
	private Animator model_animator;
	private Rigidbody rigidbody;
	void Start()
	{
			rigidbody = GetComponent<Rigidbody>();
			model_animator = playerModel.GetComponent<Animator>();
	}

	void Update()
	{
		MovePlayer();

		Jump();
	}

	// Actions
	void MovePlayer() {
		Vector3 displacement = transform.position;
		if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S)) {
			model_animator.SetBool("Moving", true);
			if (Input.GetKey(KeyCode.W)) {
				displacement += transform.forward;
			}
			if (Input.GetKey(KeyCode.S)) {
				displacement -= transform.forward;
			}
		} else {
			model_animator.SetBool("Moving", false);
		}
		transform.position = Vector3.Lerp(transform.position, displacement, speedMultiplier * Time.deltaTime);
	}

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
