using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class SearchGrid
	{
		public Vector3 cellSize { get; private set; }

		public Dictionary<Vector3Int, List<Transform>> map { get; private set; }

		private ObjectsPool<List<Transform>> _pool;

		public SearchGrid()
		{
			map = new Dictionary<Vector3Int, List<Transform>>();
			_pool = ObjectsPool<List<Transform>>.CreateObjectsPool<List<Transform>>();
		}

		public Vector3Int Position2Index(Vector3 pos) =>
			new Vector3Int(
				Mathf.FloorToInt(pos.x / cellSize.x),
				Mathf.FloorToInt(pos.y / cellSize.y),
				Mathf.FloorToInt(pos.z / cellSize.z));

		public bool AddObjectAt(Vector3 pos, Transform transform)
		{
			if (transform == null)
				return false;

			var index = Position2Index(pos);

			if (!map.TryGetValue(index, out var list))
				map[index] = list = _pool.Lock();

			list.Add(transform);

			return true;
		}

		public bool RemoveObjectAt(Vector3 pos, Transform transform)
		{
			if (transform == null)
				return false;

			var index = Position2Index(pos);

			if (map.TryGetValue(index, out var list))
			{
				list.Remove(transform);
				if (list.Count==0)
				{
					_pool.Free(list);
					map.Remove(index);
				}
				return true;
			}
			
			return false;
		}

		public List<Transform> GetObjectsInRadius(Vector3 pos, float radius, List<Transform> objects = null)
		{
			if (objects == null)
				objects = new List<Transform>();

			var min = Position2Index(
				new Vector3(
					pos.x - radius,
					pos.y - radius,
					pos.z - radius));
			var max = Position2Index(
				new Vector3(
					pos.x + radius,
					pos.y + radius,
					pos.z + radius));
			var index = Vector3Int.zero;

			for (int y = min.y; y < max.y; y++)
			for (int z = min.z; z < max.z; z++)
			for (int x = min.x; x < max.x; x++)
			{
				index.Set(x, y, z);
				
				if (map.TryGetValue(index, out var list))
					objects.AddRange(list);
			}

			return objects;
		}
	}
}