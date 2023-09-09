using System;
using System.Collections;
using Sirenix.Utilities;
using UnityEngine;

namespace RTSCore
{
	public class OrbEntity : MonoBehaviour
	{
#region General

		public Fraction   Fraction;
		public OrbProfile Profile;
		public Cooldown   Cooldown;
		public OrbInteraction Interaction;

		public float Health { get; private set; }
		public bool  IsDead { get; private set; }

		public event Action<OrbEntity, string> OnDeath;

		public void Init()
		{
			Interaction.Orb = this;
			Interaction.Init();
		}

		private void ValidateCooldown(string penaltyReason)
		{
			if (Cooldown.ActionCooldown > 0)
				ApplyPenalty(Cooldown.ActionCooldown * Time.fixedDeltaTime, penaltyReason);
		}

		internal void ApplyPenalty(float penalty, string penaltyReason)
		{
			penalty = Mathf.Abs(penalty);

			Health -= penalty;
			if (Health <= 0)
			{
				IsDead = true;
				OnDeath?.Invoke(this, penaltyReason);
				// TODO: Change orb view
			}
		}

#endregion


#region Actions

		public bool AbilitySee;       // Способность Видеть
		public bool AbilityMove;      // Способность Двигаться (Горизонтальное движение)
		public bool AbilityAttack;    // Способность Атаковать
		public bool AbilityHeal;      // Способность Лечить
		public bool AbilityStore;     // Способность Хранить
		public bool AbilityHarvester; // Способность Собирать ресурсов
		public bool AbilityCreate;    // Способность Создавать новые Орбы
		public bool AbilityDispose;   // Способность Разбирать Орбы (союзные)
		public bool AbilityFly;       // Способность Находится на высоте (Вертикальное движение)

		public void ActionMove(Vector3 position)
		{
		}
		
		public void ActionAttack(OrbEntity target)
		{
		}
		
		public void ActionHeal(OrbEntity target)
		{
		}
		
		public void ActionHarvest(ResourceEntity res, float amount)
		{
		}
		
		public void ActionTransferResource(OrbEntity target)
		{
		}
		
		public void ActionCreateOrb()
		{
		}
		
		public void ActionDisposeOrb(OrbEntity target)
		{
		}

#endregion
	}
}