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

// The value used when a method returns a new inventory object
// along with the index of the affected slot.
struct NewInventoryInfo {
	public Item[] inventory;
	public int placedIndex;

	public NewInventoryInfo(Item[] inv, int index) {
		inventory = inv;
		placedIndex = index;
	}
}

// TO-DO
// * Do we need inventory item structures? Can the UI 
public class Player_Inventory : MonoBehaviour
{
	public Transform FullInventorySlotsParent;
	public Transform QuickInventorySlotsParent;
	public int QUICK_INVENTORY_SIZE = 9;
	public int FULL_INVENTORY_SIZE = 16;
	public int TIME_UNTIL_PICKUP_AGAIN = 2;
	private Item[] QuickInventory;
	private Item[] FullInventory;
	private Inventory_Slot[] FullInventorySlots;
	private Inventory_Slot[] QuickInventorySlots;

	void Start()
	{
		Init();
	}

	// Actions
	public void PickUp(Item item) {
		if (!CanPickUp()) {
			return;
		}
		float timeElapsedSinceDrop = Time.time - item.lastDropped;
		if (timeElapsedSinceDrop < 3f) {
			return;
		}
		item.gameObject.SetActive(false);
		if (!IsInventoryFull(QuickInventory, QUICK_INVENTORY_SIZE)) {
			item.location = ItemLocation.QuickInv;
			NewInventoryInfo inventoryInfo = GetNewInventoryWithPlacedItem(QuickInventory, item);
			QuickInventory = inventoryInfo.inventory;
			QuickInventorySlots[inventoryInfo.placedIndex].Set(item, inventoryInfo.placedIndex);
			return;
		}
		if (!IsInventoryFull(FullInventory, FULL_INVENTORY_SIZE)) {
			item.location = ItemLocation.FullInv;
			NewInventoryInfo inventoryInfo = GetNewInventoryWithPlacedItem(FullInventory, item);
			FullInventory = inventoryInfo.inventory;
			FullInventorySlots[inventoryInfo.placedIndex].Set(item, inventoryInfo.placedIndex);
			return;
		}
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
		SetInventorySlots();
	}

	void SetInventorySlots() {
		FullInventorySlots = FullInventorySlotsParent.GetComponentsInChildren<Inventory_Slot>();
		QuickInventorySlots = QuickInventorySlotsParent.GetComponentsInChildren<Inventory_Slot>();
		AddCloseButtonListeners();
	}

	void AddCloseButtonListeners() {
			for (int i = 0; i < FullInventorySlots.Length; i++) {
				FullInventorySlots[i].slotCloseButton.onClick.AddListener(RemoveItemFromFullInventory(i));
			}
	}

	// Internal Helpers
	UnityAction RemoveItemFromFullInventory(int index) {
		return () => {
			FullInventory[index].gameObject.SetActive(true);
			FullInventory[index].lastDropped = Time.time;
			FullInventory[index].location = ItemLocation.None;
			FullInventory[index].transform.position = transform.position;
			FullInventory[index].StartDropForward(transform.forward);
			FullInventorySlots[index].Remove();
			FullInventory[index] = null;
		};
	}

	// Does not support full inventories. Ensure there is space in the inventory
	// before invoking to prevent strange behavior.
	NewInventoryInfo GetNewInventoryWithPlacedItem(Item[] inventory, Item item) {
		Item[] newInventory = inventory;
		bool placed = false;
		int foundIndex = 0;
		for (int i = 0; !placed && i < newInventory.Length; i++) {
			if (!newInventory[i]) {
				newInventory[i] = item;
				foundIndex = i;
				placed = true;
			}
		}
		return new NewInventoryInfo(newInventory, foundIndex);
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
