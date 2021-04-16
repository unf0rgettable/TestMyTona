using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour
{
	[SerializeField][Range(0, 100)] private int HealthUpgradeWeight = 10;
	[SerializeField][Range(0, 100)] private int DamageUpgradeWeight = 10;
	[SerializeField][Range(0, 100)] private int MoveSpeedUpgradeWeight = 5;
	[SerializeField][Range(0, 100)] private int HealWeight = 25;
	[SerializeField][Range(0, 100)] private int RifleWeight = 25;
	[SerializeField][Range(0, 100)] private int AutomaticRifleWeight = 15;
	[SerializeField][Range(0, 100)] private int ShotgunWeight = 20;

	[SerializeField] private PowerUp HealthPrefab;
	[SerializeField] private PowerUp DamagePrefab;
	[SerializeField] private PowerUp MoveSpeedPrefab;
	[SerializeField] private HealthPack HealPrefab;
	[SerializeField] private WeaponPowerUp RiflePrefab;
	[SerializeField] private WeaponPowerUp AutomaticRifleWPrefab;
	[SerializeField] private WeaponPowerUp ShotgunPrefab;

	private List<int> powerUp = new List<int>();
	private List<int> weights = new List<int>();

	private GameObject[] prefabs;

	private void Awake()
	{
		powerUp.Add(HealthUpgradeWeight);
		powerUp.Add(DamageUpgradeWeight);
		powerUp.Add(MoveSpeedUpgradeWeight);
		powerUp.Add(HealWeight);
		powerUp.Add(RifleWeight);
		powerUp.Add(AutomaticRifleWeight);
		powerUp.Add(ShotgunWeight);

		weights = CalculateWeight(powerUp);
		
		prefabs = new[]
		{
			HealthPrefab.gameObject,
			DamagePrefab.gameObject,
			MoveSpeedPrefab.gameObject,
			HealPrefab.gameObject,
			RiflePrefab.gameObject,
			AutomaticRifleWPrefab.gameObject,
			ShotgunPrefab.gameObject,
		};

		EventBus.Sub(Handle, EventBus.MOB_KILLED);
	}

	private void OnDestroy()
	{
		EventBus.Unsub(Handle, EventBus.MOB_KILLED);
	}

	private void Handle()
	{
		Spawn(PickRandomPosition());
	}

	private Vector3 PickRandomPosition()
	{
		var vector3 = new Vector3();
		vector3.x = Random.value * 11 - 6;
		vector3.y = 1;
		vector3.z = Random.value * 11 - 6;
		return vector3;
	}


	private void Spawn(Vector3 position)
	{
		var rand = Random.Range(0, weights.Last() + 1);

		if (TryGetPrefabByWeight(rand, out GameObject prefab))
		{
			if (prefab.TryGetComponent(out WeaponPowerUp weaponPowerUp))
			{
				if(weaponPowerUp.Type == Player.Instance.TypeWeapon)
					return;
			}
			Instantiate(prefab, position, Quaternion.identity);
		}
	}
	
	private bool TryGetPrefabByWeight(int weight, out GameObject prefab)
	{
		for (int i = 0; i < weights.Count - 1; i++)
		{
			if (weight >= weights[i] && weight <= weights[i + 1])
			{
				prefab = prefabs[i];
				return true;
			}
		}
		prefab = null;
		return false;
	}

	private List<int> CalculateWeight(List<int> powerUps)
	{
		List<int> tempWeights = new List<int>();
		tempWeights.Add(0);
		for (int i = 0; i < powerUps.Count; i++)
		{
			tempWeights.Add(tempWeights[i] + powerUps[i]);
		}

		return tempWeights;
	}
}