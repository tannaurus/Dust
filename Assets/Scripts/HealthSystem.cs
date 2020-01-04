using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
		public int health = 100;
		private int maxHealth = 100;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0) {
					OnDeath();
				}
    }

		public void ApplyDamage(int damage) {
			health -= damage;
			health = Mathf.Clamp(health, 0, maxHealth);
		}

		void OnDeath() {
			Debug.Log("Dead!!!");
		}
}
