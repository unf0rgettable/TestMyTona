using UnityEngine;

public class AlertController : MonoBehaviour
{
    private SpriteRenderer _alert;
    
    private Animator _animator;
    private AttackStateMachine _attackStateMachine;
    private Camera _camera;
    private void Awake()
    {
        _alert = GetComponent<SpriteRenderer>();
        _animator = GetComponentInParent<Mob>().GetComponentInChildren<Animator>();
        _attackStateMachine = _animator.GetBehaviour<AttackStateMachine>();
        _attackStateMachine.OnAttackStart = EnableAlert;
        _attackStateMachine.OnAttackFinish = DisableAlert;
        _camera = Camera.main;
    }

    private void EnableAlert()
    {
        _alert.enabled = true;
    }
    
    private void DisableAlert()
    {
        _alert.enabled = false;
    }
}
