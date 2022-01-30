using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	public class OpConstNode : GenNode
	{
		public float constant;
		
		public override void SetRandom(GenRandom random)
		{
		}

		public override float Calculate(float x, float y)
		{
			return constant;
		}
	}
}