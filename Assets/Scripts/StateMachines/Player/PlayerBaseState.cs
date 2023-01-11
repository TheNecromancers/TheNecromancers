using UnityEngine;

namespace TheNecromancers.StateMachine.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }

        protected void Move(Vector3 movement, float deltaTime)
        {
            stateMachine.Controller.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected Vector3 CalculateMovement()
        {
            Vector3 forward = stateMachine.MainCameraTransform.forward;
            Vector3 right = stateMachine.MainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputManager.MovementValue.y + right * stateMachine.InputManager.MovementValue.x;
        }

        protected void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation,
                Quaternion.LookRotation(movement),
                deltaTime * stateMachine.RotationSpeed);
        }

        protected void FaceOnTarget(float deltaTime)
        {
            if (stateMachine.Targeter.CurrentTarget == null) { return; }

            Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
            lookPos.y = 0f;

            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation,
                Quaternion.LookRotation(lookPos),
                deltaTime * stateMachine.RotationSpeed);
        }

        protected void ReturnToLocomotion()
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                Debug.Log("Go To Targeting");
                if (!stateMachine.Targeter.SelectTarget()) { return; }

                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerLocomotionState(stateMachine));
            }
        }

        protected void HandleTakeDamage()
        {
            stateMachine.Health.SetInvulnerable();
            stateMachine.SwitchState(new PlayerImpactState(stateMachine));
        }

        protected bool CanAttack()
        {
            return stateMachine.WeaponRightHand != null && stateMachine.RightHandHolder != null;
        }

        protected void OnBlock()
        {
            if (!stateMachine.HasShield()) { return; }

            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        protected void OnInteract()
        {
            if (stateMachine.InteractionDetector.CurrentTarget == null) { return; }

            stateMachine.InteractionDetector.CurrentTarget.OnInteract();
            stateMachine.SwitchState(new PlayerInteractingState(stateMachine));
            Debug.Log(stateMachine.InteractionDetector.CurrentTarget);
        }
    }
}