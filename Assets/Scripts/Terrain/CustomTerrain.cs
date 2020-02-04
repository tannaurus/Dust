using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]

public class CustomTerrain : MonoBehaviour {

	//TERRAIN --------------------------------------------------
	static int terrainSize = 5;
	public TerrainTile[,] terrain = new TerrainTile[terrainSize, terrainSize];
	public Transform tilePrefab;


	//MISC --------------------------------------------------
	public Vector2 randomHeightRange = new Vector2(0,0.1f);
	public Texture2D heightMapImage;
	public float heightMapYScale = 0.5f;


	//SETTINGS --------------------------------------------------
	public int tileSize = 500;
	public bool resetTerrain = true;


	//PERLIN NOISE ----------------------------------------------
	public float perlinXScale = 0.01f;
	public float perlinYScale = 0.01f;
	public int perlinOffsetX = 0;
	public int perlinOffsetY = 0;
	public int perlinOctaves = 3;
	public float perlinPersistance = 8;
	public float perlinHeightScale = 0.09f;

	//MULTIPLE PERLIN --------------------
	[System.Serializable]
	public class PerlinParameters
	{
		public float mPerlinXScale = 0.01f;
		public float mPerlinYScale = 0.01f;
		public int mPerlinOctaves = 3;
		public float mPerlinPersistance = 8;
		public float mPerlinHeightScale = 0.09f;
		public int mPerlinOffsetX = 0;
		public int mPerlinOffsetY = 0;
		public bool remove = false;
	}

	public List<PerlinParameters> perlinParameters = new List<PerlinParameters>()
	{
		new PerlinParameters()
	};

	void Awake()
	{
			SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
			SerializedProperty tagsProp = tagManager.FindProperty("tags");

			AddTag(tagsProp, "Terrain");
			AddTag(tagsProp, "Cloud");
			AddTag(tagsProp, "Shore");

			// apply tag changes to tag database
			tagManager.ApplyModifiedProperties();
	}

	public void Start() {
		CreateTerrain();
	}

	private void CreateTerrain() {
		terrain = new TerrainTile[terrainSize, terrainSize];
	}

	private delegate void TileCallback(TerrainTile tile);
	private void UpdateTiles(TileCallback callback) {
		// I don't think this is correct.
		// This appears to limit the size of the world to grow exponentially.
		// Can a 2D array be un-even? Should this be a more preformant data structure?
		for (int x = 0; x < terrainSize; x++) {
			for (int y = 0; y < terrainSize; y++) {
				Debug.Log(terrain[x, y]);
				if (!terrain[x, y]) {
					terrain[x, y] = GenerateTile(x, y);;
				}
				callback(terrain[x, y]);
			}
		}
	}

	private TerrainTile GenerateTile(int x, int y) {
		Vector3 tileCords = new Vector3(x * tileSize, 0, y * tileSize);
		Transform tileTransform = Instantiate(tilePrefab, tileCords, Quaternion.identity);
		tileTransform.parent = transform;
		TerrainTile tile = tileTransform.gameObject.GetComponent<TerrainTile>();
		tile.Initialize(tileSize);
		return tile;
	}

	public void Veronoi() {
		UpdateTiles(_Veronoi);
	}
	private void _Veronoi(TerrainTile tile) {
		float[,] heightMap = tile.GetHeightMap(resetTerrain);
		TerrainData tileData = tile.tileTerainData;
		int[] randomPoint = new int[]{ (int) Mathf.Round(UnityEngine.Random.Range(0, tileData.heightmapWidth)), (int) Mathf.Round(UnityEngine.Random.Range(0, tileData.heightmapWidth)) };
		heightMap[randomPoint[0], randomPoint[1]] = 100f;
		tileData.SetHeights(0, 0, heightMap);
	}

	public void Perlin() {
		UpdateTiles(_Perlin);
	}
	private void _Perlin(TerrainTile tile)
	{
		float[,] heightMap = tile.GetHeightMap(resetTerrain);
		TerrainData tileData = tile.tileTerainData;
		for (int y = 0; y < tileData.heightmapHeight; y++)
		{
			for (int x = 0; x < tileData.heightmapWidth; x++)
			{
				heightMap[x, y] += Utils.fBM((x+perlinOffsetX) * perlinXScale, (y+perlinOffsetY) * perlinYScale, perlinOctaves, perlinPersistance) * perlinHeightScale;
			}
		}

		tileData.SetHeights(0, 0, heightMap);
	}

	public void MultiplePerlinTerrain() {
		UpdateTiles(_MultiplePerlinTerrain);
	}
	private void _MultiplePerlinTerrain(TerrainTile tile)
	{
		float[,] heightMap = tile.GetHeightMap(resetTerrain);
		TerrainData tileData = tile.tileTerainData;
		for (int y = 0; y < tileData.heightmapHeight; y++)
		{
			for (int x = 0; x < tileData.heightmapWidth; x++)
			{
				foreach (PerlinParameters p in perlinParameters)
				{
					heightMap[x, y] += Utils.fBM((x + p.mPerlinOffsetX) * p.mPerlinXScale, (y + p.mPerlinOffsetY) * p.mPerlinYScale, p.mPerlinOctaves, p.mPerlinPersistance) * p.mPerlinHeightScale;
				}
			}
		}
		tileData.SetHeights(0, 0, heightMap);
	}

	public void AddNewPerlin()
	{
		perlinParameters.Add(new PerlinParameters());
	}

	public void RemovePerlin()
	{
		List<PerlinParameters> keptPerlinParameters = new List<PerlinParameters>();
		for (int i = 0; i < perlinParameters.Count; i++)
		{
			if (!perlinParameters[i].remove)
			{
				keptPerlinParameters.Add(perlinParameters[i]);
			}
		}
		if (keptPerlinParameters.Count == 0) //don't want to keep any
		{
			keptPerlinParameters.Add(perlinParameters[0]); // add at least 1
		}
		perlinParameters = keptPerlinParameters;
	}

	public void RandomTerrain() {
		UpdateTiles(_RandomTerrain);
	}
	private void _RandomTerrain(TerrainTile tile)
	{
		float[,] heightMap = tile.GetHeightMap(resetTerrain);
		TerrainData tileData = tile.tileTerainData;

		for (int x = 0; x < tileData.heightmapWidth; x++)
		{
			for (int z = 0; z < tileData.heightmapHeight; z++)
			{
				heightMap[x, z] += UnityEngine.Random.Range(randomHeightRange.x, randomHeightRange.y);
			}
		}
		tileData.SetHeights(0, 0, heightMap);
	}

	public void LoadTexture() {
		UpdateTiles(_LoadTexture);
	}
	private void _LoadTexture(TerrainTile tile)
	{
		float[,] heightMap = tile.GetHeightMap(resetTerrain);
		TerrainData tileData = tile.tileTerainData;
		Vector2 imageScale = new Vector2((float)heightMapImage.width / (float)tileData.heightmapWidth, (float)heightMapImage.height / (float)tileData.heightmapHeight);
		for (int x = 0; x < tileData.heightmapWidth; x++)
		{
			for (int z = 0; z < tileData.heightmapHeight; z++)
			{
				heightMap[x, z] = heightMapImage.GetPixel((int)((tileData.heightmapWidth - x - 1) * imageScale.x), (int)(z * imageScale.y)).grayscale * heightMapYScale;
			}
		}
		tileData.SetHeights(0, 0, heightMap);
	}

	public void ResetTerrain() {
		UpdateTiles(_ResetTerrain);
	}
	private void _ResetTerrain(TerrainTile tile)
	{
		TerrainData tileData = tile.tileTerainData;
		float[,] heightMap;
		heightMap = new float[tileData.heightmapWidth, tileData.heightmapHeight];
		for (int x = 0; x < tileData.heightmapWidth; x++)
		{
			for (int z = 0; z < tileData.heightmapHeight; z++)
			{
				heightMap[x, z] = 0;
			}
		}
		tileData.SetHeights(0, 0, heightMap);
	}

	void AddTag(SerializedProperty tagsProp, string newTag)
	{
		bool found = false;
		// ensure the tag doesn't already exist
		for (int i = 0; i < tagsProp.arraySize; i++)
		{
			SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
			if (t.stringValue.Equals(newTag)) 
			{ 
				found = true; 
				break; 
			}
		}
		// add new tag
		if (!found)
		{
				tagsProp.InsertArrayElementAtIndex(0);
				SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
				newTagProp.stringValue = newTag;
		}
	}

	public void DestroyAllTiles() {
		foreach (Transform child in transform) {
			DestroyImmediate(child.gameObject);
		}
	}
}
