using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float health = 100f;

    public void DoDamage(float damageAmount) {
        health -= damageAmount;
        if(health <= 0) Die();
    }

    void Die() {
        // death animation
        // let the game know an object has been removed
        Destroy(gameObject);
    }
}
