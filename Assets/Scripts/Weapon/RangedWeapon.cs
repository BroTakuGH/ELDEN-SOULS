using UnityEngine;

[CreateAssetMenu(fileName = "new-ranged-weapon", menuName = "weapon/new-ranged-weapon", order = 1)]
public class RangedWeapon : Weapon
{
  public override WeaponType weaponType => WeaponType.isRanged;

  [Header("Ranged Weapon Attributes")]
  public float reloadTime;
  [Range(0.0f, 1.0f)]
  public float reloadSpeed = 1f;
  public float magazineSize;
  
  public GameObject projectile;
  public float projectileSpeed;

  public override void PrintWeaponInfo()
  {
    base.PrintWeaponInfo();
    Debug.Log($"Reload Time: {reloadTime}, Reload Speed: {reloadSpeed}, Magazine Size: {magazineSize}");
  }
}
