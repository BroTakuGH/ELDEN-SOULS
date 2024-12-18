using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 2;

    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
		//gets the distance between the enemy and player
		distance = Vector2.Distance(transform.position, player.transform.position);

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

    }
}
