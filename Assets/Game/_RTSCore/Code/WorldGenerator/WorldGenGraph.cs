using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	[CreateAssetMenu(
		menuName = "RTS/World Generator",
		fileName = "WorldGenerationGraph",
		order    = 10)]
	[RequireNode(typeof(TerrainNode))]
	public class WorldGenGraph : NodeGraph
	{
		public int  seed;
		public bool dirty = false;

		public override Node AddNode(Type type)
		{
			var node = base.AddNode(type);
			dirty = true;
			return node;
		}

		public override Node CopyNode(Node original)
		{
			var node = base.CopyNode(original);
			dirty = true;
			return node;
		}

		public override void RemoveNode(Node node)
		{
			base.RemoveNode(node);
			dirty = true;
		}

		public override void Clear()
		{
			base.Clear();
			dirty = true;
		}

		public TerrainNode getTerrainNode()
		{
			for (int i = 0; i < nodes.Count; i++)
				if (nodes[i] is TerrainNode node)
					return node;
			return null;
		}
	}
}