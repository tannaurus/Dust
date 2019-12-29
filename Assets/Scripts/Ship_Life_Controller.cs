using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship_Life_Controller : MonoBehaviour
{

    void Update()
    {
				LifeController();
		}

		void LifeController() {
			float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
			if(terrainHeightWhereWeAre > transform.position.y) {
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
}
