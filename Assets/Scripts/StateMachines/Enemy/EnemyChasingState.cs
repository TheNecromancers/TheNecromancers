using UnityEngine;
using TheNecromancers.Combat;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private readonly int SpeedHash = Animator.StringToHash("Speed");

        private const float CrossFadeduration = 0.1f;
        private const float AnimatorDumpTime = 0.1f;

        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeduration);
        }

        public override void Tick(float deltaTime)
        {
            if (!IsInChaseRange())
            {
                stateMachine.SwitchState(new EnemySuspicionState(stateMachine));
                return;
            }
            else if (IsInAttackRange())
            {
                if(stateMachine.IsArcher)
                {
                    stateMachine.SwitchState(new EnemyRangedAttackState(stateMachine));
                }
                else
                {
                    stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
                }
                return;
            }

            MoveToPlayer(deltaTime);
            FaceForward(deltaTime);

            stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDumpTime, deltaTime);
        }

        public override void Exit()
        {
            ResetAgentPath();
        }

   
    }
}

