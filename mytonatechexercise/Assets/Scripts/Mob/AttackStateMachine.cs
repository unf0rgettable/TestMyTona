using System;
using UnityEngine;

public class AttackStateMachine : StateMachineBehaviour
{
    public Action OnAttackStart;
    public Action OnAttackFinish;

    private bool _isAttackStart = false;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        _isAttackStart = true;
        OnAttackStart?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.5 && _isAttackStart)
        {
            OnAttackFinish?.Invoke();
            _isAttackStart = false;
        }
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex)
    {
    }
}