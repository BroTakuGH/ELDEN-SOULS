using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.UI;

public class RogueAbilities : MonoBehaviour
{


    public PlayerHealth playerHealth;
	private EnemyChase enemyChase;
    private float attackCoolDown = 2.0f;
    private float dashCoolDown = 4.0f;
    private float waitTime = 0.2f;
    private float timer = 0.0f;

    private bool canAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyChase = GetComponent<EnemyChase>();

    }

    // Update is called once per frame
    void Update()
    {
		Debug.Log(canAttack + " Status");
		//      if(playerHealth != null) { 
		//Debug.Log(playerHealth.playerHealth);
		//}
		if (dashCoolDown >= 0) {
			dashCoolDown -= Time.deltaTime;
        }

		if (attackCoolDown >= 0) {
			attackCoolDown -= Time.deltaTime;
		}

        if (canAttack && attackCoolDown <= 0) {
            attack();
        }

		if (enemyChase != null) { 
        if (enemyChase.distance <= 4 && dashCoolDown <= 0) {
                dash();
                timer += Time.deltaTime;
                //check if the timer reached the waittime to change the speed back to default
                if (timer > waitTime) {
                    dashCoolDown += 4.0f;
                    timer = 0.0f;
				}
            

			} else {
				enemyChase.speed = 2;
                
			}
		}
	}



	void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name == "Player") {
            canAttack = true;
        }
        
	}
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "Player") {
			canAttack = false;
		}
	}

	//void OnTriggerExit(Collider other) {
	//	if (other.gameObject.name == "Player") {
	//           Debug.Log("exited");
	//	}
	//}
	void attack() {

		if (playerHealth != null) {
			playerHealth.playerHealth -= 20;
			attackCoolDown = 1.0f;
		}

	}

    void dash() {
        enemyChase.speed = 15;
    }
}
