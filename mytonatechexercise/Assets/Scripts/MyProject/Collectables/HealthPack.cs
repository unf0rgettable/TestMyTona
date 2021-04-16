using MyProject.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyProject.Collectables
{
	public class HealthPack : MonoBehaviour
	{
		[SerializeField]
		private int health = 3;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<PlayerCharacter>().Heal(health);
				Destroy(gameObject);
			}
		}
	}
}