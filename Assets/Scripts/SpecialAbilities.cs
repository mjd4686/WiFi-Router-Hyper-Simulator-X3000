using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilities : MonoBehaviour
{
    public FirstPersonAIO fpsController;
    public GameController gcScript;
    public Image dashHUD;
    public Image superjumpHUD;
    public Image slowtimeHUD;

    void Start() {
        dashHUD.enabled = false;    
        superjumpHUD.enabled = false;    
        slowtimeHUD.enabled = false;    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) { // Special Ability: DASH
            StartCoroutine(dash());
        }
        if(Input.GetKeyDown(KeyCode.E)) { // Special Ability: SUPERJUMP
            StartCoroutine(superjump());
        }
        if(Input.GetKeyDown(KeyCode.Tab)) { // Special Ability: SLOWTIME
            StartCoroutine(slowtime());
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

    // SUPERJUMP: When pressing 'E' player flies upwards supah fah and supah high
    IEnumerator superjump() {
        superjumpHUD.enabled = true;
        fpsController.fps_Rigidbody.AddForce (0,1000,0);
        yield return new WaitForSeconds(2f);
        superjumpHUD.enabled = false;
    }
    
    // SLOWTIME: When pressing 'Tab' mouse sensitivity decreases and timer pauses
    IEnumerator slowtime() {
        slowtimeHUD.enabled = true;
        fpsController.mouseSensitivity = 0.1f;
        float time = gcScript.coroutineModifier("Countdown", false, 0f); // false to stop, ignores 3rd arg
        yield return new WaitForSeconds(3f);
        gcScript.coroutineModifier("Countdown", true, time); // true to start, 3rd arg is new timer
        slowtimeHUD.enabled = false;
        fpsController.mouseSensitivity = 10;
    }
}
