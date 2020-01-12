using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item_Pickup : MonoBehaviour
{
	private Player_Inventory inventory;
	void Start()
	{
		inventory = GetComponent<Player_Inventory>();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != Item.ITEM_TAG) {
			return;
		}
		Item item = col.gameObject.GetComponent<Item>();
		if (item.location != ItemLocation.None) {
			return;
		}
		inventory.PickUp(item);
	}
}
