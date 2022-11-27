using UnityEngine;
using System;
using UnityEngine.UI;

namespace TheNecromancers.Combat
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}

