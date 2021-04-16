using MyProject.Events;
using UnityEngine;

namespace MyProject.Mob
{
	public class MobCharacter : Character.Character
	{
		public float Damage = 1;
		public float MoveSpeed = 3.5f;

		public override void TakeDamage(float amount)
		{
			if (Health <= 0)
				return;
			Health -= amount;
			OnHPChange?.Invoke(Health, -amount);
			if (Health <= 0)
			{
				Death();
			}
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

		public void Death()
		{
			EventBus.Pub(EventBus.MOB_KILLED);
			var components = GetComponents<IMobComponent>();
			foreach (var component in components)
			{
				component.OnDeath();
			}

			GetComponent<Collider>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
			Destroy(gameObject,2f);
		}
	}
}