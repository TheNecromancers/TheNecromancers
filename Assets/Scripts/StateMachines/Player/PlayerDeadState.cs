using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeadState : PlayerBaseState
{
    private readonly int DieHash = Animator.StringToHash("Die");
    private const float CrossFadeduration = 0.1f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
        Debug.Log("Player è morto");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
   
}
