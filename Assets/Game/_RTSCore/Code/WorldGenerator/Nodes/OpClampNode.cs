using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	public class OpClampNode : GenNode
	{
		[Input] public GenNode input;
		
		public float min;
		public float max;
		
		public override void SetRandom(GenRandom random)
		{
			
		}

		public override float Calculate(float x, float y)
		{
			if (input==null)
				input = GetInputValue("input", input);
			
			if (input==null)
				return .5f;
			
			return Mathf.Clamp(input.Calculate(x, y), min, max);
		}
	}
}