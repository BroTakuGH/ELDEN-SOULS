using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter{
  public void SetTarget(Transform target);
}
// Purpose is to enable a class to use 'SelectTarget.cs'.

public interface IWeapon {
  public WeaponType weaponType { get; }
}
// Purpose is to mark a class as either 'melee, ranged, magic, or unarmed.'

public interface IPickupable{
}
// Purpose is to mark a class as 'pickupable item.'