using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MetaRTS
{
	public class GenRandom
	{
		private System.Random _random;

		public void SetSeed(int seed)
		{
			_random = new Random(seed);
		}
		
		public float Value => _random.Next() / (float) Int32.MaxValue;
		
		public float Range(float min, float max)
		{
			return Mathf.Lerp(min, max, Value);
		}
	}
}