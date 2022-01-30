using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace MetaRTS
{
	public enum Operation
	{
		Add, Sub, Mul, Div, Pow, Sqrt
	}

	public class OpBinaryNode : GenNode
	{
		[Input] public GenNode inputA;
		[Input] public GenNode inputB;
		
		public Operation operation;
		public bool inverse;
		
		public override void SetRandom(GenRandom random)
		{
		}

		public override float Calculate(float x, float y)
		{
			if (inputA==null) inputA = GetInputValue("inputA", inputA);
			if (inputB==null) inputB = GetInputValue("inputB", inputB);
		
			if (inputA==null && inputB==null) return .5f;
			if (inputA==null) return inputB.Calculate(x, y);
			if (inputB==null) return inputA.Calculate(x, y);
			
			var a = inputA.Calculate(x, y);
			var b = inputB.Calculate(x, y);
			if (inverse)
				(a, b) = (b, a);
			
			switch (operation)
			{
				case Operation.Add:
					return a + b;
				case Operation.Sub:
					return a - b;
				case Operation.Mul:
					return a * b;
				case Operation.Div:
					return a / b;
				case Operation.Pow:
					return Mathf.Pow(a, b);
				case Operation.Sqrt:
					return Mathf.Pow(a, 1/b);
			}
			throw new Exception("Unknown operation!");
		}
	}
}