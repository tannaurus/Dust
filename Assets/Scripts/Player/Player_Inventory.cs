using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public Transform FullInventoryParent;
	public int QUICK_INVENTORY_SIZE = 9;
	public int FULL_INVENTORY_SIZE = 16;
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
		Debug.Log("Picking up...");
	}

	// External Helpers
	public bool CanPickUp() {
		bool qInvFull = IsInventoryFull(QuickInventory, QUICK_INVENTORY_SIZE);
		bool fInvFull = IsInventoryFull(FullInventory, FULL_INVENTORY_SIZE);
		return !qInvFull || !fInvFull;
	}

	// Updaters
	void Init() {
		QuickInventory = new Item[QUICK_INVENTORY_SIZE];
		FullInventory = new Item[FULL_INVENTORY_SIZE];
		FullInventorySlots = FullInventoryParent.GetComponentsInChildren<Inventory_Slot>();
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
				FullInventorySlots[i].Remove();
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
				FullInventorySlots[i].Set(fItem);
			}
		}
	}

	// Internal Helpers
	// TO-DO
	// * Update method to support optional specified index
	Item[] GetNewInventoryWithPlacedItem(Item[] inventory, Item item) {
		bool placed = false;
		for (int i = 0; placed || i < inventory.Length; i++) {
			if (!inventory[i]) {
				inventory[i] = item;
				placed = true;
			}
		}
		return inventory;
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
