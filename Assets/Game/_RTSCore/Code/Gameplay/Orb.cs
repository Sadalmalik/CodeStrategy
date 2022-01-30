using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetaRTS
{
	public class Orb : MonoBehaviour
	{
#region View components

		[SerializeField] private Mesh       unitModel;
		[SerializeField] private Mesh       buildingModel;
		[SerializeField] private MeshFilter view;
		[SerializeField] private Material   aliveMaterial;
		[SerializeField] private Material   deadMaterial;
		[Space]
		[SerializeField]
		public Transform trigger;
		[Space]
		[SerializeField]
		private Transform canvasRoot;
		[SerializeField] private Image healthBar;
		[SerializeField] private Image cooldownBar;

#endregion


#region Main stuff

		private GameManager _gameManager;
		private TerrainManager _terrainManager;
		
		[SerializeField] private Fraction _fraction;
		[SerializeField] private OrbAbilities _abilities;
		[SerializeField] private OrbSettings _settings;

		public Fraction Fraction => _fraction;

		public OrbAbilities Abilities => _abilities;
		
		public OrbSettings Settings => _settings;

		public float health { get; private set; }

		public bool Killed { get; private set; } = false;

		public event Action<Orb, string> OnKilled;

		private void Start()
		{
			_gameManager = GameManager.Instance;
			_terrainManager = TerrainManager.Instance;
			SetViewRadius(5);
		}

		private void Init(Fraction fraction, OrbAbilities abilities, OrbSettings settings)
		{
			_fraction = fraction;
			_abilities = abilities;
			_settings = settings;

			health = settings.maxHealth;

			SetViewRadius(settings.maxVisionDistance);
		}

		private void Update()
		{
			canvasRoot.gameObject.SetActive(_gameManager.enableInfo);

			if (!_gameManager.enableInfo)
				return;

			if (Camera.current == null)
				return;

			canvasRoot.rotation = Camera.current.transform.rotation;
		}

		private void FixedUpdate()
		{
			ValidatePosition();
			RefreshPosition();
		}

		private void SetViewRadius(float radius)
		{
			trigger.localPosition = new Vector3(0, 0.5f * radius, 0);
			trigger.localRotation = Quaternion.identity;
			trigger.localScale    = radius * new Vector3(2, 1.5f, 2);
		}

		private void ApplyPenalty(float penalty, string reason)
		{
			penalty = Mathf.Abs(penalty);

			health -= penalty;
			if (health <= 0)
			{
				Killed = true;
				OnKilled?.Invoke(this, reason);
				// TODO: Change orb view
			}
		}

#endregion


#region Action Cooldown

		private float _nextTime;

		public float ActionCooldown => Mathf.Max(0, Time.time - _nextTime);

		private void AddCooldown(float amount)
		{
			var time = Time.time;
			if (_nextTime < time)
				_nextTime = time;
			_nextTime += amount;
		}

		private void ValidateCooldown(string reason)
		{
			if (ActionCooldown > 0)
				ApplyPenalty(ActionCooldown * Time.fixedDeltaTime, reason);
		}

#endregion


#region Allies & Enemies Handling

		private HashSet<Orb>            _allies  = new HashSet<Orb>();
		private HashSet<Orb>            _enemies = new HashSet<Orb>();
		private HashSet<ResourceEntity> _sources = new HashSet<ResourceEntity>();

		private void OnTriggerEnter(Collider other)
		{
			var orb = other.GetComponentInParent<Orb>();
			if (orb != null
			 && !orb.Killed)
			{
				Debug.Log($"{gameObject.name}.OnTriggerEnter: {orb.gameObject.name}");

				if (orb.Fraction == Fraction)
					_allies.Add(orb);
				else
					_enemies.Add(orb);
			}

			var source = other.GetComponentInParent<ResourceEntity>();
			if (source != null)
				_sources.Add(source);
		}

		void OnTriggerExit(Collider other)
		{
			var orb = other.GetComponentInParent<Orb>();
			if (orb != null)
			{
				Debug.Log($"{gameObject.name}.OnTriggerEnter: {orb.gameObject.name}");

				if (orb.Fraction == Fraction)
					_allies.Remove(orb);
				else
					_enemies.Remove(orb);
			}

			var source = other.GetComponentInParent<ResourceEntity>();
			if (source != null)
				_sources.Remove(source);
		}

#endregion


#region Velocity validation

		private float   _time;
		private Vector3 _pos;

		private void RefreshPosition()
		{
			_time = Time.time;
			_pos  = transform.position;
		}

		private void ValidatePosition()
		{
			var delta = transform.position - _pos;

			var actualSpeed = delta.magnitude / (Time.time - _time);

			if (Settings.maxSpeed < actualSpeed)
				ApplyPenalty((actualSpeed - Settings.maxSpeed) * Time.fixedDeltaTime, "Move too fast!");
			
			var min = _abilities.Contains(OrbAbilities.Dive) ? _gameManager.diveHeightLimit : 0;
			var max = _abilities.Contains(OrbAbilities.Fly) ? _gameManager.heightLimit : _gameManager.flyHeightLimit;
			
			var height = _terrainManager.GetHeightAt(transform.position);
			var offset = transform.position.z - height;
			if (offset < min)
			{
				if (!_abilities.Contains(OrbAbilities.Move))
					ApplyPenalty(health * Time.fixedDeltaTime, "Forbidden underground building!");
				ApplyPenalty(offset * Time.fixedDeltaTime, "Unit too deep!");
			}
			else if (offset > max)
			{
				ApplyPenalty((offset - max) * Time.fixedDeltaTime, "Unit too hight!");
			}
		}

#endregion


#region Allowed public methods

		public Orb CreateOrb(OrbAbilities abilities, OrbSettings settings, params string[] args)
		{
			if (Killed) return null;

			ValidatePosition();
			ValidateCooldown("Try spawn while cooldown!");

			if (Killed) return null;

			//_gameManager.CreateOrb()

			return null;
		}

		public HashSet<Orb> GetVisibleAllies(HashSet<Orb> set = null)
		{
			if (Killed) return null;

			ValidatePosition();

			if (Killed) return null;

			if (set == null)
				set = new HashSet<Orb>();

			set.UnionWith(_allies);
			return set;
		}

		public void Heal(Orb otherOrb, float power)
		{
			if (Killed) return;

			ValidatePosition();
			ValidateCooldown("Try heal while cooldown!");

			if (Killed) return;

			if (!_allies.Contains(otherOrb))
			{
				ApplyPenalty(power * Time.fixedDeltaTime, "Try heal out of range!");
				return;
			}

			// TODO: Heal
			
			AddCooldown(power);
		}

		public HashSet<Orb> GetVisibleEnemies(HashSet<Orb> set = null)
		{
			if (Killed) return null;

			ValidatePosition();

			if (Killed) return null;

			if (set == null)
				set = new HashSet<Orb>();

			set.UnionWith(_enemies);
			return set;
		}

		public void Attack(Orb otherOrb, float force = 1)
		{
			if (Killed) return;

			ValidatePosition();
			ValidateCooldown("Try attack while cooldown!");

			if (Killed) return;

			if (!_enemies.Contains(otherOrb))
			{
				ApplyPenalty(force * Time.fixedDeltaTime, "Try attack out of range!");
				return;
			}

			// TODO: Attack
			
			AddCooldown(force);
		}

		public HashSet<ResourceEntity> GetVisibleSources(HashSet<ResourceEntity> set = null)
		{
			if (Killed) return null;

			ValidatePosition();

			if (Killed) return null;

			if (set == null)
				set = new HashSet<ResourceEntity>();

			set.UnionWith(_sources);
			return set;
		}

		public void Collect(ResourceEntity resourceEntity)
		{
			if (Killed) return;

			ValidatePosition();
			ValidateCooldown("Try collect resources while cooldown!");

			if (Killed) return;
		}

#endregion
	}
}