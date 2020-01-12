using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Float : MonoBehaviour
{
	public float floatHeight = 2f;
	public float floatOffset = 2f;
	public float floatSpeed = 1.2f;
	private Item item;
	private float holdHeight;
	private ItemLocation lastKnownLocation;

	void Start()
	{
		Init();
		if (item.location == ItemLocation.None) {
			InitFloat();
		}
	}

	void Update()
	{
		if (
			lastKnownLocation != ItemLocation.None &&
			item.location == ItemLocation.None
		) {
			InitFloat();
		}
		if (item.location == ItemLocation.None) {
			Float();
		}

		// Always do last.
		UpdateLastKnownLocation();
	}

	// Updaters
	void Init() {
		item = GetComponent<Item>();
		lastKnownLocation = item.location;
	}

	void InitFloat() {
		Vector3 itemPos = transform.position;
		itemPos.y = Terrain.activeTerrain.SampleHeight(transform.position);
		itemPos.y += floatHeight;
		// Keep track of this offset for the float logic
		holdHeight = itemPos.y;
		transform.position = itemPos;
	}
	
	void Float() {
		Vector3 pos = transform.position;
		float newY = (Mathf.Sin(Time.time * floatSpeed) + holdHeight) * floatOffset;
		transform.position = new Vector3(pos.x, newY, pos.z);
	}

	void UpdateLastKnownLocation() {
		if (lastKnownLocation != item.location) {
			lastKnownLocation = item.location;
		}
	}
}
