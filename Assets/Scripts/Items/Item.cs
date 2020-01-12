using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
		public int id;
		public string title;
		public Image icon;
		public KeyCode actionKey = KeyCode.F;
		public ItemLocation location = ItemLocation.None;
		public static string ITEM_TAG = "Item";

		public void Awake() {
			gameObject.tag = ITEM_TAG;
		}

		public void Update() {
			OnKeyHandler(actionKey);
		}

		// Handlers
    public void OnKeyHandler(KeyCode _key) {
			if (Input.GetKeyDown(_key) && location == ItemLocation.InHand) {
				Debug.Log("Action!");
			}
		}

}
