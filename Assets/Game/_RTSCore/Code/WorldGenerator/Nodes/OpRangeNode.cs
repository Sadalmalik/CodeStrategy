using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	public class OpRangeNode : GenNode
	{
		[Input] public GenNode input;
		
		public float pivot;
		public float dist;
		public AnimationCurve softCurve;
		
		public override void SetRandom(GenRandom random)
		{
		}

		public override float Calculate(float x, float y)
		{
			if (input==null)
				input = GetInputValue("input", input);
			
			if (input==null) return .5f;
			
			var val = input.Calculate(x, y);
			val = 1 - Mathf.Abs(val - pivot) / dist;
			return softCurve.Evaluate(val);
		}
	}
}