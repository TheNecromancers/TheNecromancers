using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyImpactState : EnemyBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Impact");
        private const float CrossFadeduration = 0.1f;

        private float duration = 1f;

        public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeduration);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if (duration <= 0)
            {
                stateMachine.GoToGuardPosition();
            }
        }

        public override void Exit() { }
    }
}
