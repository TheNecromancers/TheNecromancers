using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheNecromancers.StateMachine;
using TheNecromancers.StateMachine.Enemy;

public abstract class BossBaseState : State
{
    protected BossStateMachine stateMachine;

    public BossBaseState(BossStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
