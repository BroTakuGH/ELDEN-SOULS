using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    private const float MAX_HEALTH = 100;
    public Image healthBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth / MAX_HEALTH, 0, 1);
    }
}
