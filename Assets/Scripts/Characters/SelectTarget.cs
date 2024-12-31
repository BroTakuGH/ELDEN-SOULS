using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;



public class SelectTarget : MonoBehaviour
{
  [Space(10)]

  [Tooltip("ICharacter script.")]
  public PlayerMain player;
  public float range = 2.0f;

  [Space]
  [ReadOnly]
  [field: SerializeField]
  private Transform target;

  private void Update()
  {
      SetTarget();
  }

  private void SetTarget(){
    float closestDistance = Mathf.Infinity;
    Transform closestTarget = null;

    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);
    
    foreach (Collider2D enemy in enemies)
    {
      if (enemy.CompareTag("Enemy"))
      {
        float distance = Vector2.Distance(transform.position, enemy.transform.position);
        if (distance < closestDistance){
          closestDistance = distance;
          target = closestTarget = enemy.transform;
        }
      }
    }

    player.SetTarget(closestTarget);  
  }

  private void OnDrawGizmosSelected(){
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
