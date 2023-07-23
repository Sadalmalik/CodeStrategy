using System.Collections;
using System.Collections.Generic;
using MetaRTS_Legacy;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS_Legacy
{
	public class OpTransformNode : GenNode
	{
		[Input] public GenNode input;
		
		public float angle;
		public float scale;
		public float power;
		public Vector2 offset;
		
		public override void SetRandom(GenRandom random)
		{
			
		}

		public override float Calculate(float x, float y)
		{
			input = GetInputValue("input", input);
			
			if (input == null)
				return 0.5f;
			
			var rad = Mathf.Deg2Rad * angle;
			var sin = Mathf.Sin(rad);
			var cos = Mathf.Cos(rad);
			
			x += offset.x;
			y += offset.y;
			var nx = scale * (x * cos - y * sin);
			var ny = scale * (x * sin + y * cos);
			
			return power * input.Calculate(nx, ny);
		}
	}
}