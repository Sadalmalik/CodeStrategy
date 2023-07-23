using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MetaRTS_Legacy
{
	[NodeTint(0.5f,0.5f,0.7f)]
	public class SourcePerlinNoiseNode : GenNode
	{
		public float scale;
		public float bias;

		protected Vector2 offset;
		
		public override void SetRandom(GenRandom random)
		{
			offset = new Vector2(
					random.Range(0, 10000),
					random.Range(0, 10000)
				);
		}

		public override float Calculate(float x, float y)
		{
			var xCord = (offset.x + x) * scale;
			var yCord = (offset.y + y) * scale;
			
			return bias + Mathf.PerlinNoise(xCord, yCord);
		}
	}
}