using JetBrains.Annotations;
using System;
using System.Collections;
using System.Threading;
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


  private GameObject weaponPrefab;
  private float attackResetTimer = 0;
  private bool isAttacking = false;

  public delegate void Attack(Weapon weapon);
  private Attack attack;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		sp = GetComponent<SpriteRenderer>();
	}

	private void Update() {
    HandleMovement();
    HandleHand();
    HandleAttack();
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

  private void HandleAttack()
  {
    attackResetTimer += Time.deltaTime;
    if (isAttacking && attackResetTimer >= weapon.attackRate)
    {
      attack?.Invoke(weapon);
      attackResetTimer = 0;
    }
  }

  #region setters
  public void SetWeapon(Weapon weapon)
  {
    weapon.PrintWeaponInfo();
    this.weapon = weapon;

    SetAttack(weapon);

    ShowWeapon();
  }

  private void SetAttack(Weapon weapon)
  {
    attack = null;

    switch (weapon.weaponType)
    {
      case WeaponType.isRanged:
      attack += RangedAttack;
      break;
      case WeaponType.isMelee:
      attack += MeleeAttack;
      break;
      case WeaponType.isDisarmed:
      Debug.Log("Player is Disarmed.");
      break;
    }
  }

  private void ShowWeapon(){
    
    if (weaponPrefab != null)
    {
      Destroy(weaponPrefab);
    }
    else 
    {
      weaponPrefab = Instantiate(weapon.weaponPrefab, hand.position, hand.rotation);
      weaponPrefab.transform.SetParent(hand.transform);  
    }
  }

  public void SetTarget(Transform target){
    this.target = target;
  }
  #endregion 

  #region triggers

  /// <summary>
  /// This uses the new unity input system.
  /// Use 'Invoke Unity Events' as the Player 
  /// Input behavior and set the proper 
  /// function for the CallbackContext.
  /// </summary>

  public void TriggerAttack(InputAction.CallbackContext cbx){
    if (cbx.performed) { isAttacking = true; }
    if (cbx.canceled) { isAttacking = false; }
  }
  
  public void TriggerInteract(InputAction.CallbackContext cbx){
    Debug.Log("Interacting");
  }
  
  public void TriggerUseItem(InputAction.CallbackContext cbx){
    Debug.Log("Using Item");
  }
  #endregion

  #region attacks
  private void RangedAttack(Weapon weapon){
    RangedWeapon ranged = weapon as RangedWeapon;

    HandleProjectile(ranged.projectilePrefab, ranged.projectileSpeed);
  }
  
  private void HandleProjectile(GameObject projectile, float speed){
    Projectile current_projectile = Instantiate(projectile, hand.position, hand.transform.rotation).GetComponent<Projectile>();
    current_projectile.setup(target, speed);
  }

  private void MeleeAttack(Weapon weapon){
    MeleeWeapon melee = weapon as MeleeWeapon;


  }
  #endregion
}
