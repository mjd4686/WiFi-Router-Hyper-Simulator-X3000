using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float originalHealth;
    private float health;
    public int routerType; // 1 = Tier I, 2 = II, 3 = III hub, 4 = III beacon 

    // values of the different routers
    private float tier_i = 100f;
    private float tier_ii = 500f;
    private float tier_iii_h = 300f;
    private float tier_iii_b = 150f;

    void Start() {
        switch (routerType) {
            case 1:
                originalHealth = tier_i;
                health = tier_i;
                break;
            case 2:
                originalHealth = tier_ii;
                health = tier_ii;
                break;
            case 3:
                originalHealth = tier_iii_h;
                health = tier_iii_h;
                break;
            case 4:
                originalHealth = tier_iii_b;
                health = tier_iii_b;
                break;
            default:
                // Debug.Log("Invalid router type");
                break;
        }
    }

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
