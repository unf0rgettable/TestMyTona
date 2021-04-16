using System;
using MyProject.Events;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField]
	public Animator Animator;

	private void Awake()
	{
		EventBus<PlayerInputMessage>.Sub(AnimateRun);
		EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
	}

	private void AnimateRun(PlayerInputMessage message)
	{
		Animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
	}
	
	private void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(AnimateRun);
		EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
	}

	private void AnimateDeath()
	{
		Animator.SetTrigger("Death");
	}

	public void TriggerShoot()
	{
		Animator.SetTrigger("Shoot");
	}
}