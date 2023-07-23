using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestUnits
{
	public class BugLeg : MonoBehaviour
	{
		public  Transform targetAnchor;
		public  Transform legTransform;
		public  float     dragSpeed;
		public  float     threshold;
		public  float     thresholdRange;
		public  float     moveThreshold;
		private bool      _dragging;

		public void Start()
		{
			threshold += Random.Range(-thresholdRange, thresholdRange);
		}

		public void Tick()
		{
			var dist = Vector3.Distance(targetAnchor.position, legTransform.position);
			if (!_dragging)
			{
				_dragging = threshold < dist;
			}
			else
			{
				var coef = dragSpeed * Time.deltaTime;

				legTransform.position = Vector3.Lerp(legTransform.position, targetAnchor.position, coef);
				legTransform.rotation = Quaternion.Lerp(legTransform.rotation, targetAnchor.rotation, coef);

				_dragging = dist < moveThreshold;
			}
		}
	}
}