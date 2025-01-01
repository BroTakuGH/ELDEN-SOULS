using UnityEngine;

public class Weapon : ScriptableObject, IPickupable, IWeapon
{

  public ItemType itemType => ItemType.isWeapon;
  public virtual WeaponType weaponType => WeaponType.isDisarmed;

  [Header("Weapon Attributes")]
  public new string name;
  public string description;
  public GameObject weaponPrefab;
  [Space(15)]
  public float damage;
  [Tooltip("Time between attacks."), Range(0.1f, 0.5f)] 
  public float attackRate;

  public virtual void PrintWeaponInfo()
  {
    Debug.Log($"Weapon: {name}, Description: {description} Damage: {damage}");
  }
}