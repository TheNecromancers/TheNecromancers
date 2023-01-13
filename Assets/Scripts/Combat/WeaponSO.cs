using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New Weapon", order = 0)]
public class WeaponSO : ItemObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float[] Knockbacks { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    GameObject currentWeapon;

    public void Equip(Transform handHolder)
    {
        if(handHolder.childCount > 0)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(itemPrefab, handHolder);
    }
}
