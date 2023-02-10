using System.Collections;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public class First_AreaTrigger : AreaTrigger
{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine Player))
        {
            StartCoroutine(Transition(Player));
        }
    }

    public override IEnumerator Transition(PlayerStateMachine Player)
    {
        TransitionOff = GameObject.FindGameObjectWithTag("Transition");
        TransitionOff.GetComponentInChildren<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(0.6f);

        Loader.Load(Loader.Scene.First_Area);
        Player.SetPlayerPosition(PlayerPos);
    }
}
