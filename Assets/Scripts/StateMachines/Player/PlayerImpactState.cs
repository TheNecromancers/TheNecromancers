using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public class PlayerImpactState : PlayerBaseState
    {
        private readonly int HitHash = Animator.StringToHash("Hit");
        private const float CrossFadeduration = 0.1f;

        private float duration = 0.75f;

        public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            stateMachine.Animator.CrossFadeInFixedTime(HitHash, CrossFadeduration);
        }

        public override void Enter() { }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            duration -= deltaTime;

            if (duration <= 0)
            {
                ReturnToLocomotion();
                return;
            }
        }

        public override void Exit() { }
    }
}
