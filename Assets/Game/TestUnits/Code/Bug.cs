using System.Collections;
using UnityEngine;

namespace TestUnits
{
	public class Bug : MonoBehaviour
	{
		public BugLeg[] legs;

		public float   speed = 5f;
		public float   threshold;
		public Vector3 targetPosition;

		public void Tick()
		{
			// Move
			var self = transform;
			var dist   = Vector3.Distance(self.position, targetPosition);
			var offset = Time.deltaTime * speed;
			if (dist < offset)
			{
				self.position = targetPosition;
				var p = Random.insideUnitCircle;
				targetPosition += new Vector3(p.x * 10, 0, p.y * 10);
				self.LookAt(targetPosition);
			}
			else
			{
				self.position += offset * self.forward;
			}

			//  Animate legs
			if (legs != null)
				foreach (var leg in legs)
					leg.Tick();
		}
	}
}