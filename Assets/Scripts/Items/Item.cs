using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
		public int id;
		public string name;
		public KeyCode key = KeyCode.F;
		public bool equipped = false;

		public void OnUpdate() {
			OnKeyHandler(key);
		}

		// Getters
		public bool IsEquipped() {
			return equipped;
		}

		// Handlers
    public void OnKeyHandler(KeyCode _key) {
			if (Input.GetKeyDown(_key)) {
				Debug.Log("Equipped");
			}
		}

}
