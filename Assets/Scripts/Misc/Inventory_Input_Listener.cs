using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Input_Listener : MonoBehaviour
{
	public GameObject inventory;
	public bool inventoryOpen = false;
	void Update()
	{
		ToggleInventory();		
	}

	// Listeners
	void ToggleInventory() {
		if (Input.GetKeyDown(KeyCode.E)) {
			inventoryOpen = !inventoryOpen;
			inventory.SetActive(inventoryOpen);
		}
	}
}
