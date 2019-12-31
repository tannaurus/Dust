using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
		public float speedMultiplier = 1f; 
		public float rotationMultiplier = 10f;
		public float jumpForce = 5f;
    private Animator animator;
		private Rigidbody rigidbody;
    void Start()
    {
        animator = GetComponent<Animator>();
				rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
			// Rotate player
			if (Input.GetKey(KeyCode.A)) {
				transform.Rotate(-Vector3.up * rotationMultiplier * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.D)) {
				transform.Rotate(Vector3.up * rotationMultiplier * Time.deltaTime);
			}
			
			// Move player
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

			// Jump
			if (IsGrounded() & Input.GetKeyDown(KeyCode.Space)) {
				rigidbody.AddForce(transform.up * jumpForce);
			}
    }


		bool IsGrounded() {
			return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
		}
}
