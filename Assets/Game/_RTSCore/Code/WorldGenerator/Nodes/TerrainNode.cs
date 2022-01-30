using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace MetaRTS
{
	[NodeTint(0.5f,0.5f,0.4f)]
	public class TerrainNode : Node
	{
		[Input] public GenNode input;
		
		public GenNode Generator => input = GetInputValue("input", input);
	}
}