using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	public int id;
	public string title;
	public Sprite icon;
	public KeyCode actionKey = KeyCode.F;
	public ItemLocation location = ItemLocation.None;
	public static string ITEM_TAG = "Item";
	public float lastDropped;
	public float dropSpeed = 3f;
	public float dropDistance = 5f;

	private bool dropping = false;
	private Vector3 dropDirection;
	private Vector3 dropStart;

	private Item_Float itemFloat;

	public void Awake() {
		gameObject.tag = ITEM_TAG;
	}

	public void Start() {
		lastDropped = Time.time;
		itemFloat = GetComponent<Item_Float>();
	}

	public void Update() {
		OnKeyHandler(actionKey);
		if (
			(dropping && Time.time - lastDropped > 1f) ||
			(dropping && Vector3.Distance(dropStart, transform.position) > dropDistance)
			) {
			dropping = false;
		}
		if (dropping) {
			HandleDrop();
		}
	}

	// Actions
	public void StartDropForward(Vector3 direction) {
		dropDirection = direction;
		dropStart = transform.position;
		itemFloat.InitFloat();
		dropping = true;
	}

	// Handlers
	public void OnKeyHandler(KeyCode _key) {
		if (Input.GetKeyDown(_key) && location == ItemLocation.InHand) {
			Debug.Log("Action!");
		}
	}

	private void HandleDrop() {
		Vector3 pos = transform.position;
		Vector3 dropPositionUpdate = pos + (dropDirection * dropSpeed * Time.deltaTime);
		transform.position = dropPositionUpdate;
	}

}
