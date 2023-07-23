using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class ResourceEntity : MonoBehaviour
	{
		[SerializeField] private ResourceType _type;
		[SerializeField] private float _amount;
		
		public ResourceType Type => _type;
		
		public float Amount => _amount;
		
		public bool TryHarvest(ResourceContainer targetContainer)
		{
			return false;
		}
	}
}