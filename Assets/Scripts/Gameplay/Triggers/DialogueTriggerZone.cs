using System.Collections;
using System.Collections.Generic;
using TheNecromancers.StateMachine.Player;
using UnityEngine;

namespace TheNecromancers.StateMachine.Gameplay.Triggers
{
    public class DialogueTriggerZone : MonoBehaviour
    {
        [SerializeField] DialogueTrigger trigger;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerStateMachine Player))
            {
                trigger.StartDialogue();
                this.gameObject.SetActive(false);
            }
        }
    }
}
