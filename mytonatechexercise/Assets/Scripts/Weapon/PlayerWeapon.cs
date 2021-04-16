using MyProject.Events;
using MyProject.Player;
using UnityEngine;

namespace MyProject.Weapon
{
	public abstract class PlayerWeapon : MonoBehaviour
	{
		public WeaponSetting WeaponSett;
		public Weapons Type => WeaponSett.TypePlayerWeapon;
		public GameObject Model;
		protected virtual void Awake()
		{
			GetComponent<PlayerCharacter>().OnWeaponChange += Change;
		}

		protected virtual void OnDestroy()
		{
			EventBus<PlayerInputMessage>.Unsub(Fire);
		}

		protected void Change(Weapons type)
		{
			EventBus<PlayerInputMessage>.Unsub(Fire);
			if (type == Type)
			{
				EventBus<PlayerInputMessage>.Sub(Fire);
			}

			Model.SetActive(type == Type);
		}

		protected abstract void Fire(PlayerInputMessage message);
	}
}