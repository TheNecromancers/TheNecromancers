using System.Collections;
using System.Collections.Generic;
using TheNecromancers.Gameplay.Player;
using UnityEngine;


namespace TheNecromancers.UI.Player
{
    public class InteractionDetectorPresenter : MonoBehaviour
    {
        [SerializeField] InteractionDetector interactionDetector;

        private void Start()
        {
            interactionDetector.OnCurrentInteraction += UpdateUI;
            UpdateUI(false);
        }

        private void UpdateUI(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}

