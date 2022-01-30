using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS
{
	[ExecuteInEditMode]
	public class TerrainGeneratorV2 : MonoBehaviour
	{
		public Terrain    terrain;
		public int        seed;
		public int        deph;
		public Vector2Int size;
		public WorldGenGraph graph;
		
		[Space]
		public bool generate;
		public bool loopGenerate = false;
		
		void Update()
		{
			if (generate)
			{
				generate = loopGenerate;
				Generate();
			}
		}

		public void Generate()
		{
			var node = graph.getTerrainNode();
			var gen = node.Generator;

			float[,] heights = new float[size.x, size.y];
			for (int y = 0; y < size.y; y++)
			for (int x = 0; x < size.y; x++)
			{
				float xCord = x / (float) size.x;
				float yCord = y / (float) size.y;
				
				heights[x, y] = gen.Calculate(xCord, yCord);
			}

			var data = terrain.terrainData;
			data.heightmapResolution = Mathf.Max(size.x, size.y) + 1;
			data.size                = new Vector3(size.x, deph, size.y);
			data.SetHeights(0, 0, heights);
			terrain.terrainData = data;
		}
	}
}