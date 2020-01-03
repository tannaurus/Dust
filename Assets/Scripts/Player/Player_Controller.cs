using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
	public float speedMultiplier = 1f; 
	public float mouseSensitivityMultiplier = 1.2f;
	public int jumpForce = 1;
	public GameObject playerModel;
	private Animator model_animator;
	private Rigidbody rigidbody;
	public bool jumping = false;
	void Start()
	{
			rigidbody = GetComponent<Rigidbody>();
			model_animator = playerModel.GetComponent<Animator>();
	}

	void Update()
	{
		MovePlayer();
	}

	// Actions
	void MovePlayer() {
		Debug.Log(IsGrounded());
		Vector3 displacement = transform.position;
			if (Input.GetKey(KeyCode.W)) {
				displacement += transform.forward;
			}
			if (Input.GetKey(KeyCode.S)) {
				displacement -= transform.forward;
			}
			if (Input.GetKey(KeyCode.D)) {
				displacement += transform.right;
			}
			if (Input.GetKey(KeyCode.A)) {
				displacement -= transform.right;
			}
		if (displacement != transform.position) {
			model_animator.SetBool("Moving", true);
		} else {
			model_animator.SetBool("Moving", false);
		}
		if (IsGrounded() & Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.AddForce(displacement * jumpForce, ForceMode.Impulse);
			jumping = true;
		} else if (!jumping) {
			transform.position = Vector3.Lerp(transform.position, displacement, speedMultiplier * Time.deltaTime);
		}
	}

	// void Jump() {
	// 	if (IsGrounded() & Input.GetKeyDown(KeyCode.Space)) {
	// 		rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	// 	}
	// }
	
	// Helpers
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, 3f /* guess the player's height */ );
	}
}
