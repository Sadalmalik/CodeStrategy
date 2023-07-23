using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	[Serializable]
	public class TerrainGeneratorLayer
	{
		public bool  active;
		public int   randRange = 100;
		public float scale;
		public float bias;
		public float min;
		public float max;
		public float weight;
		[HideInInspector]
		public Vector2 offset;
		[Space]
		public TerrainGeneratorLayer[] sublayers;

		public void Init(System.Random rand)
		{
			offset = new Vector2(
					rand.Next(0, randRange),
					rand.Next(0, randRange)
					);

			foreach (var layer in sublayers)
				layer.Init(rand);
		}

		public float Calculate(float x, float y)
		{
			if (!active)
				return 0;

			float xCord = (offset.x + x) * scale;
			float yCord = (offset.y + y) * scale;
			float value = bias + Mathf.PerlinNoise(xCord, yCord);

			value = Mathf.Clamp(value, min, max);

			bool  nonZero = false;
			float sum     = 0;
			foreach (var layer in sublayers)
			{
				nonZero |= layer.active;

				sum += layer.Calculate(x, y);
			}

			if (nonZero)
				return weight * value * sum;
			return weight * value;
		}
	}

	[ExecuteInEditMode]
	public class TerrainGenerator : MonoBehaviour
	{
		public Terrain    terrain;
		public int        seed;
		public int        deph;
		public Vector2Int size;
		public AnimationCurve distanceWeight;
		
		[Space]
		public bool generate;
		public bool loopGenerate = false;
		[Space]
		public TerrainGeneratorLayer layerSetup;

		private System.Random random;

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
			random = new System.Random(seed);
			layerSetup.Init(random);

			float[,] heights = new float[size.x, size.y];
			for (int y = 0; y < size.y; y++)
			for (int x = 0; x < size.y; x++)
			{
				heights[x, y] = CalculateHeight(x, y);
			}

			var data = terrain.terrainData;
			data.heightmapResolution = Mathf.Max(size.x, size.y) + 1;
			data.size                = new Vector3(size.x, deph, size.y);
			data.SetHeights(0, 0, heights);
			terrain.terrainData = data;
		}

		public float CalculateHeight(int x, int y)
		{
			float xCord = x / (float) size.x;
			float yCord = y / (float) size.y;

			float value = layerSetup.Calculate(xCord, yCord);
			
			float weight = distanceWeight.Evaluate(new Vector2(xCord - 0.5f, yCord - 0.5f).magnitude);

			return value * weight;
		}
	}
}