using System.Collections.Generic;

namespace RTSCore
{
	public class OrbProfile
	{
		public List<int> Resources;

		// Abilities
		public bool AbilitySee;       // Способность Видеть
		public bool AbilityMove;      // Способность Двигаться (Горизонтальное движение)
		public bool AbilityAttack;    // Способность Атаковать
		public bool AbilityHeal;      // Способность Лечить
		public bool AbilityStore;     // Способность Хранить
		public bool AbilityHarvester; // Способность Собирать ресурсов
		public bool AbilityCreate;    // Способность Создавать новые Орбы
		public bool AbilityDispose;   // Способность Разбирать Орбы (союзные)
		public bool AbilityFly;       // Способность Находится на высоте (Вертикальное движение)

		public int AbilityCount =>
			(AbilitySee ? 1 : 0) +
			(AbilityMove ? 1 : 0) +
			(AbilityAttack ? 1 : 0) +
			(AbilityHeal ? 1 : 0) +
			(AbilityStore ? 1 : 0) +
			(AbilityFly ? 1 : 0) +
			(AbilityHarvester ? 1 : 0) +
			(AbilityCreate ? 1 : 0) +
			(AbilityDispose ? 1 : 0);
		
		// Parameters
		public float ParamMaxHealth;        // Здоровье
		public float ParamViewRange;        // Радиус зрения
		public float ParamInteractionRange; // Радиус взаимодействия
		public float ParamMaxSpeed;         // Скорость перемещения (горизонталь)
		public float ParamMaxHeight;        // Максимальная высота
		public float ParamMaxVerticalSpeed; // Скорость перемещения (вертикаль)
		public float ParamDamagePerSecond;  // Сила атаки
		public float ParamHealPerSecond;    // Эффективность лечения
		public float ParamProductionSpeed;  // Скорость производства
		public float ParamStorage;          // Ёмкость хранилища
	}
}