using UnityEngine;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour
{
	private ParticleSystem _particleSystem;
	private ParticleSystem.MainModule _mainModule;
	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		_mainModule = _particleSystem.main;
	}

	private void Update ()
	{
		_mainModule.startSize = transform.lossyScale.magnitude;
	}
}