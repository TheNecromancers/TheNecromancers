using TheNecromancers.StateMachine.Player;
using UnityEngine;

namespace TheNecromancers.StateMachine.Gameplay.Triggers
{
    public class CheckpointTrigger : MonoBehaviour
    {
        [SerializeField] Transform SpawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerStateMachine Player))
            {
                Debug.Log("Entrato");
                Player.LastSpawnPosition = SpawnPoint.position;
            }
        }
    }
}
