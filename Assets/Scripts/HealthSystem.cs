using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float originalHealth;
    private float health;
    public int routerType; // 1 = Tier I, 2 = II, 3 = III hub, 4 = III beacon
    public bool isDead = false;

    public AudioSource gunSounds;
    public AudioClip destroyed;

    private int difficultyLevel;

    // values of the different routers
    private float tier_i = 100f;
    private float tier_ii = 500f;
    private float tier_iii_h = 300f;
    private float tier_iii_b = 150f;

    void Start() {
        difficultyLevel = PlayerPrefs.GetInt("Difficulty");
        float multiplier = 1f;
        if(difficultyLevel == 1) multiplier = 0.5f;
        if(difficultyLevel == 3) multiplier = 1.25f; 
        switch (routerType) {
            case 1:
                originalHealth = tier_i * multiplier;
                health = tier_i * multiplier;
                break;
            case 2:
                originalHealth = tier_ii * multiplier;
                health = tier_ii * multiplier;
                break;
            case 3:
                originalHealth = tier_iii_h * multiplier;
                health = tier_iii_h * multiplier;
                break;
            case 4:
                originalHealth = tier_iii_b * multiplier;
                health = tier_iii_b * multiplier;
                break;
            default:
                // Debug.Log("Invalid router type");
                break;
        }
    }

    public void DoDamage(float damageAmount) {
        health -= damageAmount;
        if(health <= 0) {
            isDead = true;
            Die();
        }
    }

    void Die() {
        // death animation
        // let the game know an object has been removed
        var routerScore = PlayerPrefs.GetInt("routerScore");
        switch (routerType)
        {
            default:
                break;

            case 1: PlayerPrefs.SetInt("routerScore", routerScore + 100); break;
            case 2: PlayerPrefs.SetInt("routerScore", routerScore + 500); break;
            case 4: PlayerPrefs.SetInt("routerScore", routerScore + 50); break;
        }
        gunSounds.PlayOneShot(destroyed);
        Destroy(gameObject);
    }

    public float getOriginalHealth() {
        return originalHealth;
    }

    public int getRouterType() {
        return routerType;
    }
}
