using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	[NodeWidth(304)]
	public class BiomeExtractor : GenNode
	{
		[Input] public GenNode inputX;
		[Input] public GenNode inputY;
		[Input] public GenNode inputZ;
		
		public AnimationCurve distCurve;
		public Vector3 pivot;
		
		public int targetBiome;
		
		public override void SetRandom(GenRandom random)
		{
			
		}

		public override float Calculate(float x, float y)
		{
			inputX = GetInputValue("inputX", inputX);
			inputY = GetInputValue("inputY", inputY);
			inputZ = GetInputValue("inputZ", inputZ);
				
			var value = new Vector3(
				inputX?.Calculate(x, y) ?? .5f,
				inputY?.Calculate(x, y) ?? .5f,
				inputZ?.Calculate(x, y) ?? .5f);
			
			var dist = Vector3.Distance(pivot, value);
			
			return distCurve.Evaluate(1 - dist);
		}
	}
}