using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class ResourcePack : Dictionary<ResourceType, int>
	{
		public int GetFullAmount()
		{
			int value = 0;
			foreach (var pair in this)
				value += pair.Value;
			return value;
		}

		public bool Contains(ResourcePack other)
		{
			foreach (var pair in other)
				if (pair.Value > this[pair.Key])
					return false;
			return true;
		}

		public void Add(ResourcePack other)
		{
			foreach (var pair in other)
				this[pair.Key] += pair.Value;
		}

		public void Substract(ResourcePack other)
		{
			foreach (var pair in other)
				this[pair.Key] -= pair.Value;
		}
	}
}