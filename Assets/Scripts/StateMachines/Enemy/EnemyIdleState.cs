using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        private readonly int SpeedHash = Animator.StringToHash("Speed");

        private const float CrossFadeduration = 0.1f;
        private const float AnimatorDumpTime = 0.1f;

        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Health.OnTakeDamage += HandleTakeDamage;

            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeduration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if (IsInChaseRange())
            {
                if (IsInViewRange() || IsTooNearRange())
                {
                    stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                    return;
                }
            }

            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDumpTime, deltaTime);
        }

        public override void Exit() 
        {
            stateMachine.Health.OnTakeDamage -= HandleTakeDamage;

        }
    }
}