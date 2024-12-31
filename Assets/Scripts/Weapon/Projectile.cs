using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  private Transform target;
  private float speed;

  public void setup(Transform target, float speed){
    this.target = target;
    this.speed = speed;
  }

  private void Update()
  {
    GetComponent<Rigidbody2D>().velocity = transform.up * speed;

    SafeDestroy();
  }

  private void SafeDestroy()
  {
    Destroy(gameObject, 2f);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy")) 
    {
      Destroy(gameObject);
    }
  }
}
