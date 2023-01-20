using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class Area003Trigger : AreaTrigger
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            Loader.Load(Loader.Scene.Area003);
            Player.SetPlayerPosition(PlayerPos);
        }
    }
}

