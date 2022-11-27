using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
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
            if (IsInViewRange() && IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }

            suspicionTime -= deltaTime;
            Move(deltaTime);

            if (suspicionTime < 0)
            {
                if (stateMachine.PatrolPath != null)
                {
                    stateMachine.SwitchState(new EnemyPatrolState(stateMachine));
                    return;
                }
                else
                {
                    // back to initial pos
                    MoveTo(stateMachine.InitialPosition, deltaTime);
                    FaceTo(stateMachine.InitialPosition, deltaTime);

                    stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDumpTime, deltaTime);

                    if (CheckDistanceSqr(stateMachine.transform.position, stateMachine.InitialPosition, 1f))
                    {
                        stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                        return;
                    }
                }
            }

            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDumpTime, deltaTime);
        }

        public override void Exit()
        {
            ResetAgentPath();
        }
    }
}