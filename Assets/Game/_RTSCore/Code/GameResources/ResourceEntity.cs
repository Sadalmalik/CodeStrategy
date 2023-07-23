using UnityEngine;
using UnityEngine.Serialization;

namespace RTSCore
{
	public class ResourceEntity : MonoBehaviour
	{
		public ResourceType type;
		public float        amount;

		public ResourceChunk TryGet(float require)
		{
			if (amount < require)
			{
				var chunk = new ResourceChunk {type = type, amount = amount};
				amount = 0;
				Destroy(gameObject);
				return chunk;
			}
			else
			{
				amount -= require;
				return new ResourceChunk {type = type, amount = require};
			}
		}
	}

	public struct ResourceChunk
	{
		public ResourceType type;
		public float        amount;
	}
}