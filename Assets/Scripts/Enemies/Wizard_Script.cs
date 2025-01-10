using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Script : MonoBehaviour
{
	private UnitState state = UnitState.Idle;
	private GameObject player;
	private float speed = 2;

	
	public GameObject beam;
	public GameObject preBeam;
	public GameObject MagicBall;
	public Transform magicBallPos;


	private float timer;
	private float beamCooldown;
	private enum UnitState { Idle, Ability1, Chase, Attack }
	
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update() {

		beamCooldown += Time.deltaTime;

		if (beamCooldown > 4) {
			SetState(UnitState.Ability1);
		}
		else if (GetDistance() >= 15) {

			SetState(UnitState.Idle);

		} else if (GetDistance() < 15 && GetDistance() > 7) {

			SetState(UnitState.Chase);

		} else if (GetDistance() <= 7) {

			SetState(UnitState.Attack);

		}


	}

	void SetState(UnitState newState) {
		state = newState;


		switch (state) {
			case UnitState.Idle:

				Debug.Log(state);

				break;
			case UnitState.Chase:

				Debug.Log("chasing");
				transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

				break;
			case UnitState.Attack:
				Debug.Log("attacking");
				timer += Time.deltaTime;
				if (timer > 2) {

					timer = 0;
					shoot();
				}
				break;

			case UnitState.Ability1:
				shootBeam();
				beamCooldown = 0;
				break;
		}
	}

	public float GetDistance() {
		//gets the distance between the enemy and player
		return Vector2.Distance(transform.position, player.transform.position);
	}

	void shoot() {
		Instantiate(MagicBall, magicBallPos.position, Quaternion.identity);
	}

	void shootBeam() {
		Vector3 playerPosition = new Vector3(player.transform.position.x,player.transform.position.y - 1);
		Instantiate(preBeam, playerPosition, Quaternion.identity);
	}
}
