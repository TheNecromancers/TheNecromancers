using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class WeaponSO : ItemObject
{
    [field: SerializeField] public GameObject WeaponPrefab = null;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }

    public void Equip(Transform handHolder)
    {
        Instantiate(WeaponPrefab, handHolder);
    }

    
}
