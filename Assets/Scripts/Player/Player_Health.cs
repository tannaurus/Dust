using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
		private HealthSystem playerHealthSystem;
		private int previouslyKnownPlayerHealth;
    void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (previouslyKnownPlayerHealth < playerHealthSystem.health) {
					OnDamageTaken(playerHealthSystem.health - previouslyKnownPlayerHealth);
				}

				if (previouslyKnownPlayerHealth != playerHealthSystem.health) {
					previouslyKnownPlayerHealth = playerHealthSystem.health;
				}
    }

		// Helpers
		void OnDamageTaken(int damageTaken) {
			Debug.Log("You've taken damage!");
		}
}
