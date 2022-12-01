using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyAttackingState : EnemyBaseState
    {
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private const float TransitionDuration = 0.1f;

        float timeBetweenAttacks = 0f;

        public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
            stateMachine.WeaponLogic.SetAttack(stateMachine.CurrentWeapon.Damage, stateMachine.CurrentWeapon.Knockbacks[0], true);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);

            if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) {
                return; }

            timeBetweenAttacks += deltaTime;

            if (timeBetweenAttacks < stateMachine.AttackRate)
            {
                FaceToPlayer(deltaTime);
                return;
            }

            timeBetweenAttacks = 0f;

            FaceToPlayer(deltaTime);

            if (IsPlayingAnimation(stateMachine.Animator, "Attack")) { return; }


            if (!IsPlayingAnimation(stateMachine.Animator, "Attack"))
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }

        }

        public override void Exit() { }
    }
}