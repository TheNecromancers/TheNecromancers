using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuspicionState : EnemyBaseState
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float AnimatorDumpTime = 0.1f;

    private float suspicionTime;
    public EnemySuspicionState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        suspicionTime = stateMachine.SuspicionTime;
    }

    public override void Tick(float deltaTime)
    {
        suspicionTime -= deltaTime;
        Move(deltaTime);

        if (suspicionTime < 0)
        {
            if (stateMachine.PatrolPath != null)
            {
                stateMachine.SwitchState(new EnemyPatrolState(stateMachine));
                return;
            }
        }
        else if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDumpTime, deltaTime);
        }

    public override void Exit() { }
   
}
