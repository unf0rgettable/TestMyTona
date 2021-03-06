using System.Threading.Tasks;
using MyProject.Events;
using MyProject.Player;
using MyProject.Weapon;
using UnityEngine;


namespace MyProject.Weapon
{
	public class Shotgun : PlayerWeapon
	{
		private Transform _firePoint;

		protected float lastTime;

		protected override void Awake()
		{
			base.Awake();
			_firePoint = GetComponentInChildren<FirePoint>().transform;
			WeaponSett.VFX = GetComponentInChildren<ParticleSystem>();
			lastTime = Time.time - WeaponSett.Reload;
		}

		protected virtual float GetDamage()
		{
			return GetComponent<PlayerCharacter>().Damage;
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
			var directions = SpreadDirections(transform.rotation.eulerAngles, 3, 20);
			foreach (var direction in directions)
			{
				var bullet = Instantiate(WeaponSett.BulletPrefab, _firePoint.position, Quaternion.Euler(direction));
				bullet.Damage = GetDamage();
			}

			WeaponSett.VFX.Play();
		}

		public Vector3[] SpreadDirections(Vector3 direction, int num, int spreadAngle)
		{
			Vector3[] result = new Vector3[num];
			result[0] = new Vector3(0, direction.y - (num - 1) * spreadAngle / 2, 0);
			for (int i = 1; i < num; i++)
			{
				result[i] = result[i - 1] + new Vector3(0, spreadAngle, 0);
			}

			return result;
		}
	}
}