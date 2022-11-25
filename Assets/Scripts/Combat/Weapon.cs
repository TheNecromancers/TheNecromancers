using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    [SerializeField] GameObject WeaponPrefab = null;
    [field: SerializeField] public int Damage { get; private set; }

    public void Equip(Transform handTransform)
    {
        Instantiate(WeaponPrefab, handTransform);
    }

}
