using UnityEngine;

namespace RTSCore
{
	public struct Cooldown
	{
		private float _nextTime;

		public float ActionCooldown => Mathf.Max(0, Time.time - _nextTime);

		private void Add(float amount)
		{
			var time = Time.time;
			if (_nextTime < time)
				_nextTime = time;
			_nextTime += amount;
		}
	}
}