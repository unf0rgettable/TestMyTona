using System;
using MyProject.Events;
using MyProject.Weapon;

namespace MyProject.Player
{
	public class PlayerCharacter : Character.Character
	{
		public static PlayerCharacter Instance;
		public float Damage = 1;
		public float MoveSpeed = 3.5f;
		public Weapons TypeWeapon;
		public Action<Weapons> OnWeaponChange = null;

		public Action OnUpgrade = null;

		private void Start()
		{
			if (Instance != null)
			{
				DestroyImmediate(gameObject);
			}
			else
			{
				Instance = this;
			}
			
			ChangeWeapon(Weapons.AutomaticRifle);
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		public override void TakeDamage(float amount)
		{
			if (Health <= 0)
				return;
			Health -= amount;
			if (Health <= 0)
			{
				EventBus.Pub(EventBus.PLAYER_DEATH);
			}

			OnHPChange?.Invoke(Health, -amount);
		}

		public void Heal(float amount)
		{
			if (Health <= 0)
				return;
			Health += amount;
			if (Health > MaxHealth)
			{
				Health = MaxHealth;
			}

			OnHPChange?.Invoke(Health, amount);
		}


		public void Upgrade(float hp, float dmg, float ms)
		{
			Damage += dmg;
			Health += hp;
			MaxHealth += hp;
			MoveSpeed += ms;
			OnUpgrade?.Invoke();
			OnHPChange?.Invoke(Health, 0);
		}

		public void ChangeWeapon(Weapons type)
		{
			TypeWeapon = type;
			OnWeaponChange?.Invoke(type);
		}


	}
}