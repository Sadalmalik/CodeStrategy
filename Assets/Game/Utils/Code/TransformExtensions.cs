using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sadalmalik.Utils
{
	public static class TransformExtensions
	{
		public static void SetX(this Transform trans, float value)
		{
			var pos = trans.position;
			pos.x          = value;
			trans.position = pos;
		}
		
		public static void SetY(this Transform trans, float value)
		{
			var pos = trans.position;
			pos.y          = value;
			trans.position = pos;
		}
		
		public static void SetZ(this Transform trans, float value)
		{
			var pos = trans.position;
			pos.z          = value;
			trans.position = pos;
		}
		public static void SetLocalX(this Transform trans, float value)
		{
			var pos = trans.localPosition;
			pos.x               = value;
			trans.localPosition = pos;
		}
		
		public static void SetLocalY(this Transform trans, float value)
		{
			var pos = trans.localPosition;
			pos.y               = value;
			trans.localPosition = pos;
		}
		
		public static void SetLocalZ(this Transform trans, float value)
		{
			var pos = trans.localPosition;
			pos.z               = value;
			trans.localPosition = pos;
		}
	}
}
