﻿using System.Threading.Tasks;
using MyProject.Events;
using UnityEngine;

namespace MyProject.Player
{
	public class Rifle : PlayerWeapon
	{
		public override int Type => PlayerWeapon.Rifle;
		public Projectile BulletPrefab;
		public float Reload = 1f;
		public Transform FirePoint;
		public ParticleSystem VFX;

		protected float lastTime;

		protected override void Awake()
		{
			base.Awake();
			EventBus<PlayerInputMessage>.Sub(Fire);
			lastTime = Time.time - Reload;
		}

		protected virtual float GetDamage()
		{
			return GetComponent<PlayerCharacter>().Damage;
		}

		protected override async void Fire(PlayerInputMessage message)
		{
			if (Time.time - Reload < lastTime)
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

			var bullet = Instantiate(BulletPrefab, FirePoint.position, transform.rotation);
			bullet.Damage = GetDamage();
			VFX.Play();
		}
	}
}