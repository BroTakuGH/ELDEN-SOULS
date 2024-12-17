using UnityEngine;

public class Weapon : ScriptableObject, IPickupable, IWeapon
{

  public ItemType itemType => ItemType.isWeapon;
  public virtual WeaponType weaponType => WeaponType.isDisarmed;

  public new string name;
  public string description;
  public GameObject weapon;
  public float damage;
  public float attackSpeed;

  public virtual void PrintWeaponInfo()
  {
    Debug.Log($"Weapon: {name}, Description: {description} Damage: {damage}");
  }
}