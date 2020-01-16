using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemLocation {
	InHand,
	QuickInv,
	FullInv,
	None,
}

// TO-DO
// * Do we need inventory item structures? Can the UI 
public class Player_Inventory : MonoBehaviour
{
	public Transform FullInventorySlotsParent;
	public int QUICK_INVENTORY_SIZE = 9;
	public int FULL_INVENTORY_SIZE = 16;
	public int TIME_UNTIL_PICKUP_AGAIN = 2;
	private Item[] QuickInventory;
	private Item[] FullInventory;
	private Inventory_Slot[] FullInventorySlots;

	void Start()
	{
		Init();
	}

	void Update()
	{
		UpdateInventories();
	}

	// Actions
	public void PickUp(Item item) {
		if (!CanPickUp()) {
			return;
		}
		Debug.Log("Time between last drop");
		float timeElapsedSinceDrop = Time.time - item.lastDropped;
		if (timeElapsedSinceDrop < 3f) {
			return;
		}
		item.gameObject.SetActive(false);
		// if (!IsInventoryFull(QuickInventory, QUICK_INVENTORY_SIZE)) {
		//	item.location = ItemLocation.QuickInv;
		// 	QuickInventory = GetNewInventoryWithPlacedItem(QuickInventory, item);
		// 	return;
		// }
		if (!IsInventoryFull(FullInventory, FULL_INVENTORY_SIZE)) {
			item.location = ItemLocation.FullInv;
			FullInventory = GetNewInventoryWithPlacedItem(FullInventory, item);
			return;
		}
		Debug.Log("Picking up...");
	}

	// External Helpers
	public bool CanPickUp() {
		bool qInvFull = IsInventoryFull (QuickInventory, QUICK_INVENTORY_SIZE);
		bool fInvFull = IsInventoryFull(FullInventory, FULL_INVENTORY_SIZE);
		return !qInvFull || !fInvFull;
	}

	// Updaters
	void Init() {
		QuickInventory = new Item[QUICK_INVENTORY_SIZE];
		FullInventory = new Item[FULL_INVENTORY_SIZE];
		SetInventorySlots();
	}

	void SetInventorySlots() {
		FullInventorySlots = FullInventorySlotsParent.GetComponentsInChildren<Inventory_Slot>();
		AddCloseButtonListeners();
	}

	void UpdateInventories() {
		UpdateQuickInventory();
		UpdateFullInventory();
	}

	// Loops through the quick inventory monitoring each item's location and updating them accordinly
	// - TO-DO
	// * Make QuickInventory UI and update the UI here
	void UpdateQuickInventory() {
		for (int i = 0; i < QuickInventory.Length; i++) {
			Item qItem = QuickInventory[i];
			if (!qItem) {
				return;
			}
			if (qItem.location == ItemLocation.FullInv) {
				QuickInventory[i] = null; 
				FullInventory = GetNewInventoryWithPlacedItem(FullInventory, qItem); 
			} else if (qItem.location == ItemLocation.None) {
				QuickInventory[i] = null;
			}
		}
	}

	// Loops through the full inventory monitoring each item's location and updating them accordinly 
	// - TO-DO
	// * 
	void UpdateFullInventory() {
		for (int i = 0; i < FullInventory.Length; i++) {
			Item fItem = FullInventory[i];
			if (!fItem) {
				if (FullInventorySlots[i].Populated()) {
					FullInventorySlots[i].Remove();
				}
				return;
			}

			// Change locations of items
			if (fItem.location == ItemLocation.QuickInv) {
				FullInventory[i] = null; 
				QuickInventory = GetNewInventoryWithPlacedItem(QuickInventory, fItem); 
			} else if (fItem.location == ItemLocation.None) {
				FullInventory[i] = null;
			}

			// Make sure what's displayed in the UI is up to date
			if (
				FullInventorySlots[i].Populated() &&
				FullInventorySlots[i].itemId != fItem.id
			) {
				FullInventorySlots[i].Set(fItem, i);
			} else if (!FullInventorySlots[i].Populated()) {
				FullInventorySlots[i].Set(fItem, i);
			}
		}
	}

	void AddCloseButtonListeners() {
			for (int i = 0; i < FullInventorySlots.Length; i++) {
				FullInventorySlots[i].slotCloseButton.onClick.AddListener(RemoveItemFromFullInventory(i));
			}
	}

	// Internal Helpers
	// delegate void Del(); 
	UnityAction RemoveItemFromFullInventory(int index) {
		return () => {
			FullInventory[index].gameObject.SetActive(true);
			FullInventory[index].lastDropped = Time.time;
			FullInventory[index].location = ItemLocation.None;
			FullInventory[index].transform.position = transform.position;
			FullInventory[index].StartDropForward(transform.forward);
			FullInventory[index] = null;
		};
	}

	// TO-DO
	// * Update method to support optional specified index
	Item[] GetNewInventoryWithPlacedItem(Item[] inventory, Item item) {
		Item[] dupInventory = inventory;
		bool placed = false;
		for (int i = 0; !placed && i < dupInventory.Length; i++) {
			if (!dupInventory[i]) {
				dupInventory[i] = item;
				placed = true;
			}
		}
		return dupInventory;
	}

	bool IsInventoryFull(Item[] inventory, int maxLength) {
		int count = 0;
		foreach(Item item in inventory) {
			if (!!item) {
				count++;
			}
		}
		return count == maxLength;
	}
}
