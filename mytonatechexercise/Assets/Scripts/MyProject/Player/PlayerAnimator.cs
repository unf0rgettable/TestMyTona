using MyProject.Events;
using UnityEngine;

namespace MyProject.Player
{
	public class PlayerAnimator : MonoBehaviour
	{
		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
			EventBus<PlayerInputMessage>.Sub(AnimateRun);
			EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
		}

		private void AnimateRun(PlayerInputMessage message)
		{
			_animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
		}
	
		private void OnDestroy()
		{
			EventBus<PlayerInputMessage>.Unsub(AnimateRun);
			EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
		}

		private void AnimateDeath()
		{
			_animator.SetTrigger("Death");
		}

		public void TriggerShoot()
		{
			_animator.SetTrigger("Shoot");
		}
	}
}