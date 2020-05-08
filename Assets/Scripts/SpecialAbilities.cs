using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilities : MonoBehaviour
{
    public FirstPersonAIO fpsController;
    public Image dashHUD;
    public Image superjumpHUD;

    void Start() {
        dashHUD.enabled = false;    
        superjumpHUD.enabled = false;    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) { // Special Ability: DASH
            StartCoroutine(dash());
        }
        if(Input.GetKeyDown(KeyCode.E)) { // Special Ability: SUPERJUMP
            StartCoroutine(superjump());
        }
    }

    // DASH: When pressing 'Q' and 'W', the player should move forward at 25x speed for 0.25s
    IEnumerator dash() {
        dashHUD.enabled = true;
        fpsController.useStamina = false;
        fpsController.walkSpeed = 100f;
        yield return new WaitForSeconds(0.25f);
        fpsController.useStamina = true;
        fpsController.walkSpeed = 4f;
        dashHUD.enabled = false;
    }

    // SUPERJUMP: When pressing 'E' player flies upwards
    IEnumerator superjump() {
        superjumpHUD.enabled = true;
        fpsController.fps_Rigidbody.AddForce (0,1000,0);
        yield return new WaitForSeconds(2f);
        superjumpHUD.enabled = false;
    }
    
}
