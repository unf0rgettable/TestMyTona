using UnityEngine;

namespace MyProject.Mob
{
    public class AlertController : MonoBehaviour
    {
        private SpriteRenderer _alert;
        private Animator _animator;
        private AttackStateMachine _attackStateMachine;
        private void Awake()
        {
            _alert = GetComponent<SpriteRenderer>();
            _animator = GetComponentInParent<global::MyProject.Mob.MobCharacter>().GetComponentInChildren<Animator>();
            _attackStateMachine = _animator.GetBehaviour<AttackStateMachine>();
            _attackStateMachine.OnAttackStart = EnableAlert;
            _attackStateMachine.OnAttackFinish = DisableAlert;
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
}
