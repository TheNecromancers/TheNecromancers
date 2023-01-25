using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class Area02Trigger : AreaTrigger
{
    public EnemiesManager EnemiesManager;

    private void Start()
    {
        EnemiesManager = FindObjectOfType<EnemiesManager>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            //EnemiesManager = FindObjectOfType<EnemiesManager>();
            EnemiesManager.EnemiesDead();
            Loader.Load(Loader.Scene.Area002);
            Player.SetPlayerPosition(PlayerPos);
        }
    }
}
