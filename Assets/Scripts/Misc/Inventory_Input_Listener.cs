using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Input_Listener : MonoBehaviour
{
	public GameObject inventory;
	private bool isInventoryOpen = false;
	void Update()
	{
		ToggleInventory();		
	}

	// Listeners
	void ToggleInventory() {
		if (Input.GetKeyDown(KeyCode.E)) {
			isInventoryOpen = !isInventoryOpen;
			inventory.SetActive(isInventoryOpen);
		}
	}
}
