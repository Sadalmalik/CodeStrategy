using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class TerrainManager : Singleton<TerrainManager>
	{
		public Terrain terrain;

		private Vector3 ToTerrainPosition(Vector3 pos)
		{
			Vector3 size   = terrain.terrainData.size;
			Vector3 offset = terrain.transform.position;

			return new Vector3(
					(pos.x - offset.x) / size.x,
					(pos.y - offset.y) / size.y,
					(pos.z - offset.z) / size.z
				);
		}

		public float GetHeightAt(Vector3 pos)
		{
			return terrain.SampleHeight(pos);
		}

		public Vector3 GetNormalAt(Vector3 pos)
		{
			var pivot = ToTerrainPosition(pos);
			
			return terrain.terrainData.GetInterpolatedNormal(pivot.x, pivot.z);
		}
	}
}