using MyProject.Player;
using MyProject.Weapon;
using UnityEngine;

namespace MyProject.Collectables
{
    public class WeaponPowerUp : MonoBehaviour
    {
        [SerializeField]
        private Weapons type;

        public Weapons Type => type;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerCharacter>().ChangeWeapon(Type);
                Destroy(gameObject);
            }
        }
    }
}