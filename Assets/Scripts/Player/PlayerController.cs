using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [Header("Player")]
	public Joystick movementJoystick;
	public float playerSpeed;
	private Rigidbody2D rb;
	private SpriteRenderer sp;
	public Animator animator;

  [Header("Items - Scriptable Object")]
  public Weapon weapon;
  
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		sp = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate() {
    HandleMovement();
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

  #region setters
  public void SetWeapon(Weapon weapon){
    weapon.PrintWeaponInfo();
    this.weapon = weapon;
  }
  #endregion 

  #region unity_input_system
  public void OnHandleAttack(InputAction.CallbackContext cbx){
    Debug.Log("Attacking");

    switch(weapon.weaponType){
      case WeaponType.isMelee:
        StartCoroutine(AttackMelee());
        break;
      case WeaponType.isRanged:
        StartCoroutine(AttackRanged());
        break;
      case WeaponType.isDisarmed:
        Debug.Log("Player is Disarmed");
        break;
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
  private IEnumerator AttackRanged(){
    Debug.Log("Pew!");
    yield return new WaitForSeconds(weapon.attackSpeed);

    // ranged-weapon-features...
  }

  private IEnumerator AttackMelee(){
    Debug.Log("Slash!");
    yield return new WaitForSeconds(weapon.attackSpeed);

    // melee-weapon-features...
  }
  #endregion
}
