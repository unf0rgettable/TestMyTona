using MyProject.Player;
using UnityEngine;

namespace MyProject.Collectables
{
    public class WeaponPowerUp : MonoBehaviour
    {
        [SerializeField]
        private int type;

        public int Type => type;

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