using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
	public Joystick movementJoystick;
	public float playerSpeed;
	private Rigidbody2D rb;
	private SpriteRenderer SpriteRenderer;
	public Animator animator;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		if (movementJoystick.Direction.y != 0) {
			rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
			SpriteRenderer = GetComponent<SpriteRenderer>();
			animator.SetFloat("speed", playerSpeed);
			bool flipped = movementJoystick.Direction.x < 0;
			SpriteRenderer.flipX = flipped;
		} else {
			rb.velocity = Vector2.zero;
			animator.SetFloat("speed", 0);
		}
	}
}
