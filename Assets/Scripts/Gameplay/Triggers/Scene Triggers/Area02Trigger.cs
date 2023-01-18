using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class Area02Trigger : AreaTrigger
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            Loader.Load(Loader.Scene.Area002_Cloned);
            Player.SetPlayerPosition(PlayerPos);
        }
    }
}
