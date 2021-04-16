using UnityEngine;

namespace MyProject.Weapon
{
    [CreateAssetMenu(fileName = "NewWeaponSetting", menuName = "WeaponSetting", order = 0)]
    public class WeaponSetting : ScriptableObject
    {
        [SerializeField] private Weapons typePlayerWeapon;
        public Weapons TypePlayerWeapon => typePlayerWeapon;

        [SerializeField] private Projectile bulletPrefab;
        public Projectile BulletPrefab => bulletPrefab;
        
        [SerializeField] private float reload;
        public float Reload => reload;

        [SerializeField] private ParticleSystem _VFX;
        public ParticleSystem VFX
        {
            get => _VFX;
            set => _VFX = value;
        }
    }
}