using UnityEngine;
using TheNecromancers.Combat;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class WeaponSO : ItemObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float[] Knockbacks { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }


    public void Equip(Transform handHolder)
    {
        Instantiate(itemPrefab, handHolder);
    }
}
