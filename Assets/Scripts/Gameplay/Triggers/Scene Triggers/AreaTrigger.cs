using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

public abstract class AreaTrigger : MonoBehaviour
{
    public Vector3 PlayerPos;
    public GameObject TransitionOff;

    public abstract void OnTriggerEnter(Collider other);

    public abstract IEnumerator Transition(PlayerStateMachine player);
}
