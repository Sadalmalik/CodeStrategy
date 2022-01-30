using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS
{
	public class ResourceContainer
	{
		private ResourcePack _pack;
		public int Capacity { get; private set; }
		public int Free => Capacity - _pack.GetFullAmount();
		
		public ResourceContainer(int capacity)
		{
			Capacity = capacity;
			_pack = new ResourcePack();
		}
		
		public bool Contains(ResourcePack pack) => _pack.Contains(pack);
		
		public ResourcePack Store(ResourcePack pack)
		{
			var sum = _pack.GetFullAmount() + pack.GetFullAmount();
			
			//if (sum)
			
			return pack;
		}
	}
}