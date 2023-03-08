using TheNecromancers.Gameplay.Player;
using TMPro;
using UnityEngine;

namespace TheNecromancers.UI.Player
{
    public class InteractionDetectorPresenter : MonoBehaviour
    {
        [SerializeField] InteractionDetector InteractionDetector;
        [SerializeField] TextMeshProUGUI InteractionText;

        private void OnEnable()
        {
            InteractionDetector.OnCurrentInteraction += UpdateUI;
            UpdateUI(InteractionDetector.CurrentTarget);
        }

        private void UpdateUI(IInteractable CurrentTarget)
        {
            if (CurrentTarget != null)
            {
                InteractionText.enabled = CurrentTarget.IsInteractable;
            }
            else
            {
                InteractionText.enabled = false;
            }
        }
    }
}

