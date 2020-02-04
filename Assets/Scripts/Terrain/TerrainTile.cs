using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
		public Vector2 worldCords;
		public Terrain tileTerrain;
		public TerrainData tileTerainData;

		public void Initialize(int size) {
			tileTerrain = GetComponent<Terrain>();
			tileTerainData = tileTerrain.terrainData;
			tileTerainData.size = new Vector3(size, 600, size);
			this.gameObject.tag = "Terrain";
		}
		
		public bool AmIHere(Vector2 here) {
			return worldCords == here;
		}

		public float[,] GetHeightMap(bool resetTerrain) {
			if (resetTerrain)
			{
					return new float[tileTerainData.heightmapWidth, tileTerainData.heightmapHeight];
			}
			else {
					return tileTerainData.GetHeights(0, 0, tileTerainData.heightmapWidth, tileTerainData.heightmapHeight);
			}   
		}
}
