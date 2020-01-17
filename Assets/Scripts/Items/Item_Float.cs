using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Float : MonoBehaviour
{
	private float floatHeight = 1f;
	private float floatOffset = 0.2f;
	private float floatSpeed = 2f;
	private Item item;
	private float holdHeight;

	void Start()
	{
		Init();
		if (item.location == ItemLocation.None) {
			InitFloat();
		}
	}

	void Update()
	{
		if (item.location == ItemLocation.None) {
			Float();
		}
	}

	// Updaters
	void Init() {
		item = GetComponent<Item>();
	}

	public void InitFloat() {
		Vector3 itemPos = transform.position;
		itemPos.y = Terrain.activeTerrain.SampleHeight(transform.position);
		itemPos.y = itemPos.y + floatHeight;
		// Keep track of this offset for the float logic
		holdHeight = itemPos.y;
		transform.position = itemPos;
	}
	
	void Float() {
		Vector3 pos = transform.position;
		pos.y = (Mathf.Sin(Time.time * floatSpeed)) * floatOffset + holdHeight;
		transform.position = pos;
	}
}
