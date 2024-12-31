using UnityEngine;

public class Weapon : ScriptableObject, IPickupable, IWeapon
{

  public ItemType itemType => ItemType.isWeapon;
  public virtual WeaponType weaponType => WeaponType.isDisarmed;

  [Header("Weapon Attributes")]
  [Tooltip("Name of the weapon.")]
  public new string name;
  [Tooltip("Short description of the weapon.")] 
  public string description;
  [Tooltip("Sprite of the weapon.")]
  public Sprite weaponSprite;
  [Space(15), Tooltip("Damage of the weapon.")]
  public float damage;
  [Tooltip("Time between attacks."), Range(0.1f, 0.5f)] 
  public float attackRate;

  public virtual void PrintWeaponInfo()
  {
    Debug.Log($"Weapon: {name}, Description: {description} Damage: {damage}");
  }
}