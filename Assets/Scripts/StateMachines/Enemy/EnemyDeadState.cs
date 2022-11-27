using UnityEngine;

namespace TheNecromancers.StateMachine.Enemy
{
    public class EnemyDeadState : EnemyBaseState
    {
        private readonly int DieHash = Animator.StringToHash("Die");
        private const float CrossFadeduration = 0.1f;
        public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            Debug.Log("Nemico Morto");
            stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
            stateMachine.WeaponLogic.gameObject.SetActive(false);
            stateMachine.Controller.enabled = false;
            GameObject.Destroy(stateMachine.Target);
        }

        public override void Tick(float deltaTime) { }

        public override void Exit() { }
    }
}