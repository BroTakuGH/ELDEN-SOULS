using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  [Header("Scriptable Object")]
  public ScriptableObject item;
  
  [Header("Visual Effects")]
  public GameObject vfx;
  public float duration;
  
  private void OnTriggerEnter2D(Collider2D other){
    if (other.CompareTag("Player")){
      PlayerController player = other.GetComponent<PlayerController>();
      Pickup(player);
    }
  }

  private void Pickup(PlayerController player){
    HandleVisualEffects();
    switch(item){
      case Weapon weapon:
        player.SetWeapon(weapon);
        break;
      // other-ipickupable-classes
    }
    DisableComponents();
    Destroy(gameObject, duration);
  }

  private void HandleVisualEffects(){
    Destroy(Instantiate(vfx, transform.position, transform.rotation), duration);
  }

  private void DisableComponents()
  {
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<BoxCollider2D>().enabled = false;

    foreach (Transform child in transform)
      child.gameObject.SetActive(false);
  }
}
