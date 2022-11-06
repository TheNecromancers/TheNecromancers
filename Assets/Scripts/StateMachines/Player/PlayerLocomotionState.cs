using UnityEngine;

public class PlayerLocomotionState : PlayerBaseState
{
    private readonly int LocomotionTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float AnimatorDumpTime = 0.1f;
    private const float CrossFadeDuration = 0.3f;

    public PlayerLocomotionState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.SetFloat(SpeedHash, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.MovementSpeed, deltaTime);

        if (stateMachine.InputManager.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDumpTime, deltaTime);
            return;
        }


        stateMachine.Animator.SetFloat(SpeedHash, stateMachine.Controller.velocity.magnitude, AnimatorDumpTime, deltaTime);
        Debug.Log(stateMachine.Controller.velocity.magnitude);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
    }


}