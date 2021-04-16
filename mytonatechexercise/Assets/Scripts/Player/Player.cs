using System;
using MyProject.Events;

public class Player : Character
{
	public static Player Instance;
	public float Damage = 1;
	public float MoveSpeed = 3.5f;
	public int TypeWeapon = 0;
	public Action<int> OnWeaponChange = null;

	public Action OnUpgrade = null;

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	public void TakeDamage(float amount)
	{
		if (Health <= 0)
			return;
		Health -= amount;
		if (Health <= 0)
		{
			EventBus.Pub(EventBus.PLAYER_DEATH);
		}

		OnHPChange?.Invoke(Health, -amount);
	}

	public void Heal(float amount)
	{
		if (Health <= 0)
			return;
		Health += amount;
		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}

		OnHPChange?.Invoke(Health, amount);
	}


	public void Upgrade(float hp, float dmg, float ms)
	{
		Damage += dmg;
		Health += hp;
		MaxHealth += hp;
		MoveSpeed += ms;
		OnUpgrade?.Invoke();
		OnHPChange?.Invoke(Health, 0);
	}

	public void ChangeWeapon(int type)
	{
		TypeWeapon = type;
		OnWeaponChange?.Invoke(type);
	}


}