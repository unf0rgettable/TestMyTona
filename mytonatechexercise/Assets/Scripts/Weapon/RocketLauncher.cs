using System.Threading.Tasks;
using MyProject.Events;
using MyProject.Player;
using MyProject.Weapon;
using UnityEngine;

namespace MyProject.Weapon
{
    public class RocketLauncher : PlayerWeapon
    {
        private Transform _firePoint;
        protected float lastTime;

        protected override void Awake()
        {
            base.Awake();
            _firePoint = GetComponentInChildren<FirePoint>().transform;
            lastTime = Time.time - WeaponSett.Reload;
        }

        protected virtual float GetDamage()
        {
            return GetComponent<PlayerCharacter>().Damage * 2;
        }

        protected override async void Fire(PlayerInputMessage message)
        {
            if (Time.time - WeaponSett.Reload < lastTime)
            {
                return;
            }

            if (!message.Fire)
            {
                return;
            }

            lastTime = Time.time;
            GetComponent<PlayerAnimator>().TriggerShoot();

            await Task.Delay(16);

            var bullet = Instantiate(WeaponSett.BulletPrefab, _firePoint.position, transform.rotation);
            bullet.Damage = GetDamage();
            WeaponSett.VFX.Play();
        }
    }
}