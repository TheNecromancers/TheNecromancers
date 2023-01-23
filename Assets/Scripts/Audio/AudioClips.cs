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
   [SerializeField] public AudioClip[] CrossbowShoot;
   [SerializeField] public AudioClip[] Death;
   [SerializeField] public AudioClip[] BodyDrop;
   [SerializeField] public AudioClip[] Hurt;
   [SerializeField] public AudioClip[] EnemyGroan;
   [SerializeField] public AudioClip[] PowerUp1;
   [SerializeField] public AudioClip[] PowerUp2;

}
