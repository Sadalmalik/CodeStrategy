using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS_Legacy
{
	[NodeTint(0.5f,0.5f,0.7f)]
	public class SourceConstNode : GenNode
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