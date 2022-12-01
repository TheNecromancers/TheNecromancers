using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyStunState : EnemyBaseState
    {
        private readonly int ImpactHash = Animator.StringToHash("Stun");
        private const float CrossFadeduration = 0.1f;

        private float duration;

        public EnemyStunState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Sono dentro Impact");
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeduration);
            duration = stateMachine.StunDuration;
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
