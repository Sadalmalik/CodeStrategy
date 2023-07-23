using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class UnitHeightLocker : MonoBehaviour
	{
		private TerrainManager _terrainManager;

		public bool applyOnUpdate;
		public float heightOverGround;

		void Start()
		{
			_terrainManager = TerrainManager.Instance;
		}

		void Update()
		{
			if (applyOnUpdate)
				Apply();
		}
		
		public void Apply()
		{
			var pos    = transform.position;
			var height = _terrainManager.GetHeightAt(pos);

			pos.y = height + heightOverGround;

			transform.position = pos;
		}
	}
}