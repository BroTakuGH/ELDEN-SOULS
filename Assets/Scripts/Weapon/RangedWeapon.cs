using System.Diagnostics.CodeAnalysis;
using Unity.Burst.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "new-ranged-weapon", menuName = "weapon/new-ranged-weapon", order = 1)]
public class RangedWeapon : Weapon
{
  public override WeaponType weaponType => WeaponType.isRanged;

  public float reloadTime;
  [Tooltip("Modifer for reload time based on certain conditions."), Min(0.5f)]
  public float reloadTimeMultiplier = 1.0f;
  public int maximumAmmo;
  
  [NotNull]
  public GameObject projectilePrefab;
  
  [Min(1.0f)]
  public float projectileSpeed = 1.0f;

  [Space]

  [Header("Debugging")]
  [field: SerializeField]
  private int currentAmmo;

  public override void PrintWeaponInfo()
  {
    base.PrintWeaponInfo();
    Debug.Log($"Reload Time: {reloadTime}, Reload Speed: {reloadTimeMultiplier}, Magazine Size: {maximumAmmo}");
  }
}
