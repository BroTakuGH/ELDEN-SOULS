using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterTag{
  Player,
  Enemy
}
// Purpose is to mark what is the 'Unity Tag' of a character game object.

public enum WeaponType{
  isMelee,
  isRanged,
  isMagic,
  isDisarmed
}
// Purpose is to mark what type of weapon a subclass 'Weapon (scriptable object)' is. 

public enum ItemType{
  isWeapon,
  isHealth
}
// Purpose is to mark what type of item an 'IPickupable' class is.



