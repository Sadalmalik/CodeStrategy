using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace MetaRTS_Legacy
{
	[NodeTint(0.5f, 0.5f, 0.7f)]
	public class SourceRadialNode : GenNode
	{
		public bool origin;
		public float distance;
		public AnimationCurve curve;
		
		public override void SetRandom(GenRandom random)
		{
		}

		public override float Calculate(float x, float y)
		{
			if (origin)
			{
				return curve.Evaluate(new Vector2(x, y).magnitude / distance);
			}
			else
			{
				x = x * 2 - 1f;
				y = y * 2 - 1f;
				return curve.Evaluate(new Vector2(x, y).magnitude / distance);
			}
		}
	}
}