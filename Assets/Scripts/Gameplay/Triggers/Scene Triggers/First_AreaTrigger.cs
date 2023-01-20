using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class First_AreaTrigger : AreaTrigger
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            Loader.Load(Loader.Scene.First_Area);
            Player.SetPlayerPosition(PlayerPos);
        }
    }
}
