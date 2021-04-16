using MyProject.Mob;
using MyProject.Player;
using UnityEngine;

namespace MyProject
{
	public class Projectile : MonoBehaviour
	{
		public float Damage;
		public float Speed = 8;
		public bool DamagePlayer = false;
		public bool DamageMob;
		public float TimeToLive = 5f;
		private float timer = 0f;
		private bool destroyed = false;

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (destroyed)
			{
				return;
			}

			if (DamagePlayer && other.CompareTag("Player"))
			{
				other.GetComponent<PlayerCharacter>().TakeDamage(Damage);
				destroyed = true;
			}

			if (DamageMob && other.CompareTag("Mob"))
			{
				var mob = other.GetComponent<MobCharacter>();
				if (PlayerCharacter.Instance.TypeWeapon == PlayerWeapon.RocketLauncher)
				{
					GetComponent<Explosion>().Exp(transform.position);
					var charactersCollider = Physics.OverlapSphere(transform.position, 2);
					foreach (var character in charactersCollider)
					{
						if (character.TryGetComponent(out Character.Character characterType))
						{
							characterType.TakeDamage(Damage);
						}
					}				
				}
				else
				{
					mob.TakeDamage(Damage);
				}

				Destroy(gameObject);
			}
		}

		protected virtual void Update()
		{
			if (!destroyed)
			{
				transform.position += transform.forward * Speed * Time.deltaTime;
			}

			timer += Time.deltaTime;
			if (timer > TimeToLive)
			{
				Destroy(gameObject);
			}
		}
	}
}