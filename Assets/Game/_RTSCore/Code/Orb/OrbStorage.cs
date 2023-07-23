using System.Collections.Generic;
using System.Linq;

namespace RTSCore
{
	public class OrbStorage
	{
		public OrbProfile Profile;

		public Dictionary<ResourceType, float> Storage = new Dictionary<ResourceType, float>();

		public float AvailableCapacity { get; private set; }

		public void RefreshCapacity()
		{
			float used = Storage.Values.Sum();
			AvailableCapacity = Profile.ParamStorage - used;
		}

		public void AddResource(ResourceChunk chunk)
		{
			Storage.TryGetValue(chunk.type, out float amount);
			Storage[chunk.type] = amount + chunk.amount;

			AvailableCapacity += chunk.amount;
		}

		public ResourceChunk RequireResource(ResourceType type, float require)
		{
			if (!Storage.ContainsKey(type))
				return new ResourceChunk {type = type, amount = 0};

			var available = Storage[type];
			if (available < require)
			{
				AvailableCapacity -= available;
				var chunk = new ResourceChunk {type = type, amount = available};
				Storage[type] = 0;
				return chunk;
			}

			AvailableCapacity -= require;

			Storage[type] = available - require;
			return new ResourceChunk {type = type, amount = require};
		}
	}
}