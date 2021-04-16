using MyProject.Player;
using UnityEngine;

namespace MyProject.Collectables
{
	public class HealthPack : MonoBehaviour
	{
		[SerializeField]
		private int Health;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<PlayerCharacter>().Heal(Health);
				Destroy(gameObject);
			}
		}
	}
}