using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class Area005Trigger : AreaTrigger
{
    public EnemiesManager EnemiesManager;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            EnemiesManager = FindObjectOfType<EnemiesManager>();
            EnemiesManager.EnemiesDead();
            Loader.Load(Loader.Scene.Area005);
            Player.SetPlayerPosition(PlayerPos);
        }
    }
}
