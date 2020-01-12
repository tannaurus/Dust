using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
	// In an empty slot, itemId's will still be an int.
	public int itemId;
	public string itemTitle;
	public Image itemIcon;
	public bool populated;

	// Getters
	public bool Populated() {
		return populated;
	}

	// Actions
	public void Remove() {
		if (!populated) {
			return;
		}
		Debug.Log("Removed!");
		populated = false;
		itemId = 0;
		itemTitle = null;
		itemIcon = null;
	}

	public void Set(Item item) {
		Debug.Log("Set!");
		populated = true;
		itemId = item.id;
		itemTitle = item.title;
		itemIcon = item.icon;
	}
}
