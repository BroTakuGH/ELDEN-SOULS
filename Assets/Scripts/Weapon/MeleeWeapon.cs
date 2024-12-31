using UnityEngine;

[CreateAssetMenu(fileName = "new-melee-weapon", menuName = "weapon/new-melee-weapon", order = 1)]
public class MeleeWeapon : Weapon
{
  public override WeaponType weaponType => WeaponType.isMelee;

  [Tooltip("How far the push of the weapon.")]
  public float knockback;

  public override void PrintWeaponInfo()
  {
    base.PrintWeaponInfo();
    Debug.Log($"Knockback: {knockback}");
  }
}
