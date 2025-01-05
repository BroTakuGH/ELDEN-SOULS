using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 2;
   

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

		transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
	}

	public float GetDistance() {
		//gets the distance between the enemy and player
		return Vector2.Distance(transform.position, player.transform.position);
	}
}
