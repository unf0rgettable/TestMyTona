using System.Collections;
using MyProject.Events;
using MyProject.Player;
using MyProject.Utils;
using UnityEngine;

namespace MyProject.Mob
{
	[RequireComponent(typeof(MobMover))]
	[RequireComponent(typeof(global::MyProject.Mob.MobCharacter))]
	public class MeleeAttack : MonoBehaviour, IMobComponent
	{
		[SerializeField]
		private float AttackDistance = 1f;
		[SerializeField]
		private float DamageDistance = 1f;

		public float AttackDelay = 1f;

		private MobMover mover;
		private global::MyProject.Mob.MobCharacter _mobCharacter;
		private MobAnimator mobAnimator;
		private bool attacking = false;
		private Coroutine _attackCoroutine = null;

		private void Awake()
		{
			_mobCharacter = GetComponent<global::MyProject.Mob.MobCharacter>();
			mover = GetComponent<MobMover>();
			mobAnimator = GetComponent<MobAnimator>();
			EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
		}

		private void OnDestroy()
		{
			EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
		}

		private void Update()
		{
			if (attacking)
			{
				return;
			}

			var playerDistance = (transform.position - PlayerCharacter.Instance.transform.position).Flat().magnitude;
			if (playerDistance <= AttackDistance)
			{
				attacking = true;
				_attackCoroutine = StartCoroutine(Attack());
			}
		}

		private IEnumerator Attack()
		{
			mobAnimator.StartAttackAnimation();
			mover.Active = false;
			yield return new WaitForSeconds(AttackDelay);
			var playerDistance = (transform.position - PlayerCharacter.Instance.transform.position).Flat().magnitude;
			mover.Active = true;
			attacking = false;
			_attackCoroutine = null;
			if (playerDistance <= DamageDistance)
			{
				if (TryGetComponent(out Kamikadze kamikadze))
				{
					GetComponent<Explosion>().Exp(transform.position);
					var colliders = Physics.OverlapSphere(transform.position, 2);
					foreach (var collider in colliders)
					{
						if (collider.TryGetComponent(out Character.Character characterType))
						{
							characterType.TakeDamage(_mobCharacter.Damage);
						}
					}
					GetComponent<Character.Character>().TakeDamage(666);
				}
				else
				{
					PlayerCharacter.Instance.TakeDamage(_mobCharacter.Damage);
				}
			}
		}

		public void OnDeath()
		{
			enabled = false;
			if (_attackCoroutine != null)
			{
				StopCoroutine(_attackCoroutine);
			}
		}
	}
}