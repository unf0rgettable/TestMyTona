using UnityEngine;

public class AlertController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer alert;
    
    private Animator _animator;
    private AttackStateMachine _attackStateMachine;
    private Camera _camera;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _attackStateMachine = _animator.GetBehaviour<AttackStateMachine>();
        _attackStateMachine.OnAttackStart = EnableAlert;
        _attackStateMachine.OnAttackFinish = DisableAlert;
        _camera = Camera.main;
    }

    private void EnableAlert()
    {
        alert.enabled = true;
    }
    
    private void DisableAlert()
    {
        alert.enabled = false;
    }
    
    private void LateUpdate()
    {
        alert.transform.rotation = _camera.transform.rotation;
    }
}
