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
            StartCoroutine(Transition(Player));
        }
    }

    public override IEnumerator Transition(PlayerStateMachine player)
    {
        TransitionOff = GameObject.FindGameObjectWithTag("Transition");
        TransitionOff.GetComponentInChildren<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(0.6f);

        Loader.Load(Loader.Scene.Area003);
        player.SetPlayerPosition(PlayerPos);
    }
}

