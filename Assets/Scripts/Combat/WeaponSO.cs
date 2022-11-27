using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] GameObject WeaponPrefab = null;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Knockback { get; private set; }

    public void Equip(Transform handHolder)
    {
        Instantiate(WeaponPrefab, handHolder);
    }
}
