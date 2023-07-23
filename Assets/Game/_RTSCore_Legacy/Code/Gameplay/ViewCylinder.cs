using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class ViewCylinder : MonoBehaviour
	{
		private float _radius;

		public Transform trigger;

		public float Radius
		{
			get => _radius;
			set
			{
				_radius = value;

				trigger.localPosition = new Vector3(0, 0.5f * value, 0);
				trigger.localRotation = Quaternion.identity;
				trigger.localScale    = new Vector3(2 * value, 1.5f * value, 2 * value);
			}
		}
	}
}