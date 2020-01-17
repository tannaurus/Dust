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
	public int index;

	public Button slotButton;
	public Image slotImage;
	public Button slotCloseButton;

	public void Start() {
		// slotCloseButton.onClick.AddListener(Remove);
	}

	// Getters
	public bool Populated() {
		return populated;
	}

	// Actions
	public void Remove() {
		if (!populated) {
			return;
		}
		populated = false;
		itemId = 0;
		itemTitle = null;
		slotImage.sprite = null;
		slotImage.enabled = false;
		slotCloseButton.interactable = false;
		index = 0;
	}

	public void Set(Item item, int inventoryIndex) {
		if (populated) {
			return;
		}
		populated = true;
		itemId = item.id;
		itemTitle = item.title;
		slotImage.sprite = item.icon;
		slotImage.enabled = true;
		slotCloseButton.interactable = true;
		index = inventoryIndex;
	}
}
