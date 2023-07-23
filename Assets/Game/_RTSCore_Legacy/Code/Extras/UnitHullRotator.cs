using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class UnitHullRotator : MonoBehaviour
	{
		private TerrainManager _terrainManager;

		public bool applyOnUpdate;
		public Transform directionSource;

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
			var normal = _terrainManager.GetNormalAt(pos);
			
			transform.rotation =
				Quaternion.LookRotation(
					normal,
					directionSource.forward)
			  * Quaternion.Euler(-90, 0, 180);
		}
	}
}