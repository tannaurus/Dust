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

	void Start()
	{
		Init();
	}

	void Update()
	{
		Float();
	}

	// Updaters
	void Init() {
		item = GetComponent<Item>();
		Vector3 itemPos = transform.position;
		itemPos.y = Terrain.activeTerrain.SampleHeight(transform.position);
		Debug.Log(itemPos.y);
		itemPos.y += floatHeight;
		// Keep track of this offset for the float logic
		holdHeight = itemPos.y;
		Debug.Log(itemPos.y);
		transform.position = itemPos;
	}

	void Float() {
		Vector3 pos = transform.position;
		float newY = (Mathf.Sin(Time.time * floatSpeed) + holdHeight) * floatOffset;
		transform.position = new Vector3(pos.x, newY, pos.z);
	}
}
