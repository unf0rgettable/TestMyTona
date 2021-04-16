using MyProject.Events;
using MyProject.Mob;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyProject.Systems
{
	public class MobSpawner : Handler<SpawnMobMessage>
	{
		public MobCharacter[] Prefabs;

		protected override void Awake()
		{
			base.Awake();
			EventBus.Sub(Unsub, EventBus.PLAYER_DEATH);
		}

		protected override void HandleMessage(SpawnMobMessage message)
		{
			var position = new Vector3(Random.value * 11 - 6, 1, Random.value * 11 - 6);
			Instantiate(Prefabs[message.Type], position, Quaternion.identity);
		}
	}
}