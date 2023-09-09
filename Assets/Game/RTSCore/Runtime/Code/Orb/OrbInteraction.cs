using System.Collections.Generic;
using UnityEngine;

namespace RTSCore
{
	public class OrbInteraction : MonoBehaviour
	{
		public OrbEntity Orb;
		public Fraction  Fraction;

		private HashSet<OrbEntity>      _allies  = new HashSet<OrbEntity>();
		private HashSet<OrbEntity>      _enemies = new HashSet<OrbEntity>();
		private HashSet<ResourceEntity> _sources = new HashSet<ResourceEntity>();

		internal void Init()
		{
			Fraction = Orb.Fraction;
		}
		
		private void OnTriggerEnter(Collider collider)
		{
			var other = collider.GetComponentInParent<OrbEntity>();
			if (other != null && !other.IsDead)
			{
				Debug.Log($"{gameObject.name}.OnTriggerEnter: {other.gameObject.name}");

				if (other.Fraction == Orb.Fraction)
					_allies.Add(other);
				else
					_enemies.Add(other);
			}

			var source = other.GetComponentInParent<ResourceEntity>();
			if (source != null)
				_sources.Add(source);
		}

		void OnTriggerExit(Collider collider)
		{
			var other = collider.GetComponentInParent<OrbEntity>();
			if (other != null)
			{
				Debug.Log($"{gameObject.name}.OnTriggerEnter: {other.gameObject.name}");

				if (other.Fraction == Orb.Fraction)
					_allies.Remove(other);
				else
					_enemies.Remove(other);
			}

			var source = other.GetComponentInParent<ResourceEntity>();
			if (source != null)
				_sources.Remove(source);
		}
	}
}