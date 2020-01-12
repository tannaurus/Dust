using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
	// In an empty slot, itemId's will still be an int.
	public int itemId;
	public string itemTitle;
	public bool populated;

	public Button slotButton;
	public Image slotImage;
	public Button slotCloseButton;

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
		slotImage.sprite = null;
		slotImage.enabled = false;
	}

	public void Set(Item item) {
		if (populated) {
			return;
		}
		Debug.Log("Set!");
		populated = true;
		itemId = item.id;
		itemTitle = item.title;
		slotImage.sprite = item.icon;
		slotImage.enabled = true;
	}
}
