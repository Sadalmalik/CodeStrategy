using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class Checker : MonoBehaviour
	{
		public TerrainManager terrainManager;

		public Transform marker;

		void Start()
		{
			terrainManager = TerrainManager.Instance;
		}

		void Update()
		{
			if (marker == null)
				return;

			var pos    = transform.position;
			var height = terrainManager.GetHeightAt(pos);
			var normal = terrainManager.GetNormalAt(pos);

			pos.y           = height;
			marker.position = pos;
			
			marker.rotation =
				Quaternion.LookRotation(
					normal,
					transform.forward)
			  * Quaternion.Euler(-90, 0, 180);
		}
	}
}