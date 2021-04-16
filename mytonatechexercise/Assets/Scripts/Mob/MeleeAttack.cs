using System;
using System.Collections;
using MyProject.Events;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MobMover))]
[RequireComponent(typeof(Mob))]
public class MeleeAttack : MonoBehaviour, IMobComponent
{
	public float AttackDistance = 1f;
	public float DamageDistance = 1f;

	public float AttackDelay = 1f;

	private MobMover mover;
	private Mob mob;
	private MobAnimator mobAnimator;
	private bool attacking = false;
	private Coroutine _attackCoroutine = null;

	private void Awake()
	{
		mob = GetComponent<Mob>();
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

		var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
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
		var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
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
	                if (collider.TryGetComponent(out Character characterType))
	                {
		                characterType.TakeDamage(mob.Damage);
	                }
                }
        		GetComponent<Character>().TakeDamage(666);
        	}
            else
            {
	            Player.Instance.TakeDamage(mob.Damage);
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