using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class beamScript : MonoBehaviour
{

	private GameObject player;
	private PlayerHealth playerHealth;

	private float damageOverTime;
	private bool isTouchingPlayer;


	// Start is called before the first frame update
	void Start()
    {

		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();

		Destroy(gameObject, 5);

	}

    // Update is called once per frame
    void Update()
    {
		if (isTouchingPlayer && damageOverTime <= 0) {
			damage();

		}
		damageOverTime -= Time.deltaTime;

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.name == "Player") 
		{
			isTouchingPlayer = true;
			damageOverTime = 0;
		}

	}

	private void OnTriggerExit2D(Collider2D other) {

		if (other.gameObject.name == "Player") {
			isTouchingPlayer = false;
		}
	}

	void damage() {
		playerHealth.playerHealth -= 20;
		damageOverTime = 1;
		Debug.Log("damaged");

	}
}
