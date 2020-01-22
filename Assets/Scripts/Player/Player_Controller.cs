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
	public float fallMultiplier = 1f;
	public float lowJumpMultipler = 1f;

	// Will disable this controller to be overwritten by another.
	public bool forceOverride = false;
	void Start()
	{
			rigidbody = GetComponent<Rigidbody>();
			model_animator = playerModel.GetComponent<Animator>();
	}

	void Update()
	{
		if (forceOverride) {
			return;
		}
		MovePlayer();
		JumpGravityModifer();
	}

	// Actions
	void MovePlayer() {
		Vector3 modifiedPosition = transform.position;
		if (Input.GetKey(KeyCode.W)) {
			modifiedPosition += transform.forward;
		}
		if (Input.GetKey(KeyCode.S)) {
			modifiedPosition -= transform.forward;
		}
		if (Input.GetKey(KeyCode.D)) {
			modifiedPosition += transform.right;
		}
		if (Input.GetKey(KeyCode.A)) {
			modifiedPosition -= transform.right;
		}

		UpdateAnimator(modifiedPosition);

		// Jump
		Vector3 displacement = transform.position - modifiedPosition;
		if (IsGrounded() & Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.velocity += (displacement + Vector3.up) * jumpForce;
		}

		if (modifiedPosition.y < Terrain.activeTerrain.SampleHeight(modifiedPosition)) {
			modifiedPosition.y = Terrain.activeTerrain.SampleHeight(modifiedPosition);
		}
		
		transform.position = Vector3.Lerp(transform.position, modifiedPosition, speedMultiplier * Time.deltaTime);
	}

	void JumpGravityModifer() {
		if (rigidbody.velocity.y < 0) {
			rigidbody.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
		} else if (rigidbody.velocity.y > 0) {
			rigidbody.velocity += Vector3.up * Physics.gravity.y * lowJumpMultipler * Time.deltaTime;
		}
	}

	void UpdateAnimator(Vector3 playerMovement) {
		if (playerMovement != transform.position) {
			model_animator.SetBool("Moving", true);
		} else {
			model_animator.SetBool("Moving", false);
		}
		model_animator.SetFloat("VerticalVelocity", rigidbody.velocity.y);
		model_animator.SetFloat("ForwardMovement", playerMovement.z - transform.position.z);
		model_animator.SetFloat("HorizontalMovement", playerMovement.x - transform.position.x);
		model_animator.SetBool("Grounded", IsGrounded());
	}
	
	// Helpers
	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, 3f /* guess the player's height */ );
	}
}
