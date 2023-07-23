using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public static class BitUtils
	{
		public static int CountBits(int bitmap)
		{
			int bits = 0;
			while (bitmap != 0)
			{
				var bit = bitmap & 1;
				if (bit != 0) bits++;
				bitmap >>= 1;
			}
			return bits;
		}
	}
}