using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Script : MonoBehaviour
{
	private UnitState state = UnitState.Idle;
	private GameObject player;
	public GameObject arrow;
	public Transform arrowPos;
	public float speed = 2;

	private float timer;
	private enum UnitState { Idle, Patrol, Chase, Attack }
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {

		if (GetDistance()  >= 10) {
				
			SetState(UnitState.Idle);

		}
		else if (GetDistance() < 10 && GetDistance() > 4) {

			SetState(UnitState.Chase);

		} else if (GetDistance() <= 4) {

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
		}
	}

	public float GetDistance() {
		//gets the distance between the enemy and player
		return Vector2.Distance(transform.position, player.transform.position);
	}

	void shoot() {
		Instantiate(arrow, arrowPos.position, Quaternion.identity);
	}
}
