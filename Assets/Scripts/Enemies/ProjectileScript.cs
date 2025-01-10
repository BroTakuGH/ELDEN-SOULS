using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealth>();

		Vector3 direction = player.transform.position - rb.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 4;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other) {

        
		if (other.gameObject.name == "Player") {
			playerHealth.playerHealth -= 20;
            Destroy(gameObject);
		}

	}
	
}
