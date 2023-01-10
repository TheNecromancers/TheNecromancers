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
            stateMachine.EnemyPresenter.ShowExclamationMark();
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
                stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
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

        private bool IsInAttackRange()
        {
            if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

            return CheckDistanceSqr(stateMachine.Player.transform.position, stateMachine.transform.position, stateMachine.AttackRange);
        }
    }
}

