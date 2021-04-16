using MyProject.Events;
using UnityEngine;

namespace MyProject.Player
{
	public class PlayerMover : MonoBehaviour
	{
		private void Awake()
		{
			EventBus<PlayerInputMessage>.Sub(AnimateRun);
		}

		private void AnimateRun(PlayerInputMessage message)
		{
			float speed = GetComponent<PlayerCharacter>().MoveSpeed;
			Vector3 delta = new Vector3(speed * message.MovementDirection.x, 0, speed * message.MovementDirection.y) *
			            Time.deltaTime;
			transform.position += delta;
			transform.forward = new Vector3(message.AimDirection.x, 0, message.AimDirection.y);
		}
	
		private void OnDestroy()
		{
			EventBus<PlayerInputMessage>.Unsub(AnimateRun);
		}
	}
}