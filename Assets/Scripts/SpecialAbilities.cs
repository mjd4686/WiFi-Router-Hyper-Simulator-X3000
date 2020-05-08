using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilities : MonoBehaviour
{
    public FirstPersonAIO fpsController;
    public Image dashHUD;

    void Start() {
        dashHUD.enabled = false;    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) { // Special Ability: DASH
            StartCoroutine(dash());
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


}
