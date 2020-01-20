using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
		public HealthBar healthBar;
		private float health = 100f;
		private float maxHealth = 100f;


    void Update()
    {
        if (health <= 0) {
					OnDeath();
				}
				healthBar.health = health;
    }

		public void ApplyDamage(float damage) {
			health -= damage;
			health = Mathf.Clamp(health, 0f, maxHealth);
		}

		void OnDeath() {
			Debug.Log("Dead!!!");
		}
}
