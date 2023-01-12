using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData/New AudioData", order = 1)]
public class AudioClips : ScriptableObject
{
   [SerializeField] public AudioClip[] Attacks;
   [SerializeField] public AudioClip[] Footsteps;
   [SerializeField] public AudioClip[] Parry;
   [SerializeField] public AudioClip[] Roll;
}
