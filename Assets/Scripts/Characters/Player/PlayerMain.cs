using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SelectTarget))]
public class PlayerMain : MonoBehaviour, ICharacter
{
  [Space(10)]

  [NotNull]
	public Joystick movementJoystick;
	public float playerSpeed;
	private Rigidbody2D rb;
	private SpriteRenderer sp;
	public Animator animator;
  public Transform hand;

  [Space]
  [CanBeNull]
  public Weapon weapon;
  
  [Space]
  [ReadOnly]
  [field: SerializeField]
  private Transform target;

  private Coroutine attack;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		sp = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate() {
    HandleMovement();
    HandleHand();
  }

  private void HandleMovement() {
    if (movementJoystick.Direction.y != 0)
    {
      rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
      animator.SetFloat("speed", playerSpeed);
      bool flipped = movementJoystick.Direction.x < 0;
      sp.flipX = flipped;
    }
    else
    {
      rb.velocity = Vector2.zero;
      animator.SetFloat("speed", 0);
    }
  }

  private void HandleHand()
  {
    if (rb.velocity != Vector2.zero) 
    {
      Quaternion rotation;

      if (target != null)
      {
        Vector3 directionToTarget =  target.position - hand.transform.position; 

        Quaternion targetRotation = Quaternion.LookRotation(hand.transform.forward, directionToTarget);
        rotation = Quaternion.RotateTowards(hand.transform.rotation, targetRotation, 15f);
      } 
      else 
      {
        Quaternion targetRotation = Quaternion.LookRotation(hand.transform.forward, rb.velocity.normalized);
        rotation = Quaternion.RotateTowards(hand.transform.rotation, targetRotation, 15f);  
      }
      
      hand.rotation = rotation;
    }
  }

  #region setters
  public void SetWeapon(Weapon weapon){
    weapon.PrintWeaponInfo();
    this.weapon = weapon;
    
    // 'ShowWeapon()'
  }

  private void ShowWeapon(){
    Instantiate(weapon.weaponSprite, hand.position, hand.rotation);
  }

  public void SetTarget(Transform target){
    this.target = target;
  }
  #endregion 

  #region unity_input_system
  public void OnHandleAttack(InputAction.CallbackContext cbx){
    if (cbx.performed){
      Debug.Log("Attacking");

      switch (weapon.weaponType){
        case WeaponType.isRanged:
        {
          if (weapon is RangedWeapon ranged_weapon)
          attack = StartCoroutine(AttackRanged(ranged_weapon));
          break;
        }
        case WeaponType.isMelee:
        {
          if (weapon is MeleeWeapon melee_weapon)
          attack = StartCoroutine(AttackMelee(melee_weapon));
          break;
        }
        case WeaponType.isDisarmed:
          Debug.Log("Player is Disarmed");
          break;
      }
    } else if (attack != null) {
      StopCoroutine(attack);
    }
  }
  
  public void OnHandleInteract(InputAction.CallbackContext cbx){
    Debug.Log("Interacting");
  }
  
  public void OnHandleUseItem(InputAction.CallbackContext cbx){
    Debug.Log("Using Item");
  }
  #endregion
  
  #region coroutines
  private IEnumerator AttackRanged(RangedWeapon weapon){
    while (true){
      Debug.Log("Pew!");
    
      HandleProjectile(weapon.projectilePrefab, weapon.projectileSpeed);
      yield return new WaitForSeconds(weapon.attackRate);
    }
  }
  
  private void HandleProjectile(GameObject projectile, float speed){
    Projectile current_projectile = Instantiate(projectile, hand.position, hand.transform.rotation).GetComponent<Projectile>();
    current_projectile.setup(target, speed);
  }

  private IEnumerator AttackMelee(MeleeWeapon weapon){
    while (true){
      Debug.Log("Slash!");
      yield return new WaitForSeconds(weapon.attackRate);

      // 'melee weapon features' 
    }
  }
  #endregion
}
