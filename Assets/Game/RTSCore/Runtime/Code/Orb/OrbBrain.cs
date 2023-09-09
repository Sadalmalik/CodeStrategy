using System;
using UnityEngine;

namespace RTSCore
{
	public abstract class OrbBrain : MonoBehaviour
	{
		public OrbEntity Orb;

		private int _failPerSecondCounter;

		public void OnSecond()
		{
			_failPerSecondCounter = 0;
		}
		
		public void SafeTick()
		{
			try
			{
				Tick();
			}
			catch (Exception e)
			{
				Debug.Log(e);
				if (_failPerSecondCounter > 10)
				{
					Orb.ApplyPenalty(10, "Too many errors!");
				}
			}
		}

		public abstract void Tick();
	}
}