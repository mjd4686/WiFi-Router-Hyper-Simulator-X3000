﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] routers;
    public GameObject[] beacons;

    // crosshair
    public Image crosshairEngaged;

    // sound effects
    public AudioSource gunSounds;
    public AudioClip gunshot;
    public AudioClip destroyed;
    public AudioClip damaged;
    public AudioClip overheatHiss;

    // damage
    private float projectileDamage = 30f;

    // ROF
    private float rateOfFire = 0.2f;
    private float nextShot = 0f;
    public float range = 200f;

    // cooldown
    public int shotsToOverheat = 15;
    private int currentShotsFired;
    public float cooldownTimer = 1f;
    private bool isCooling = false;
    public Animator gunAnimation;
    public Text cooldownHUD;
    public Text isCoolingDownHUD;

    // particles for firing and impact
    public ParticleSystem muzzleFlash;
    public GameObject impact;

    // game clock
    public Text timerLabel;
    public float currTime; // for slowtime special ability in SpecialAbilities.cs
    
    private int difficultyLevel;

    // Start is called before the first frame update
    void Start() {
        routers = GameObject.FindGameObjectsWithTag("Router");
        difficultyLevel = PlayerPrefs.GetInt("Difficulty");
        float clockSeconds = 60f;
        if(difficultyLevel == 0) clockSeconds = 120f;
        if(difficultyLevel == 2) clockSeconds = 45f;
        StartCoroutine ("Countdown", clockSeconds);
        currentShotsFired  = 0;
        crosshairEngaged.enabled = false;
        gunSounds = GetComponent<AudioSource>();
    }

    private IEnumerator Countdown (float time) { // coroutine responsible for game timer, counts down and displays in the canvas HUD. Ends if all routers are destroyed.
        while (time >= 0f) {
            if (routers.Length == 0) {
                Debug.Log("All routers destroyed!");
                // Score Calculation Here.  
                PlayerPrefs.SetInt("timeLeft", (int)time);
                PlayerPrefs.SetInt("completed", 1);
                SceneManager.LoadScene("ScoreBoard");
            } else {
                timerLabel.text = string.Format ("{0} seconds", time);
            }
            currTime = time;
            time--;
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Countdown Complete!");
        SceneManager.LoadScene("ScoreBoard");
    }

    // modify the coroutines from another script (currently only used by SpecialAbilities slowtime)
    // returns the current time right now, which is makes it less useful for other purposes
    // add more args if you need to pass new coroutine arguments, inelegant but whatever
    public float coroutineModifier(string coroutine, bool startOrStop, float coroutineFloatArg) {
        // startOrStop : true is for starting, false for stopping
        if(startOrStop) StartCoroutine(coroutine, coroutineFloatArg);
        else StopCoroutine(coroutine);
        
        return currTime;
    }

    // Update is called once per frame
    void Update() {
        // check if cooling down before checking whether you need to cool, then if needed start cooldown timer
        if (isCooling) {
            gunAnimation.SetBool("firing", false);
            return;
        }
        if (currentShotsFired >= shotsToOverheat) {
                StartCoroutine(cooldown());
    
                return;
        }

        // Fire if mouse held
        if (Input.GetMouseButton(0) && Time.time > nextShot){
            nextShot = Time.time + rateOfFire;
            currentShotsFired += 1;
            StartCoroutine(overheatHUD());
            fire();
        } else {
            gunAnimation.SetBool("firing", false);            
        }
        
        //routers & beacons
        routers = GameObject.FindGameObjectsWithTag("Router");
        beacons = GameObject.FindGameObjectsWithTag("Beacon");

        // checks if you've killed all the beacons or their hub
        List<GameObject> tierIIIHub = new List<GameObject>();
        foreach(var router in routers) {
            HealthSystem routerMaxHealth = router.GetComponent<HealthSystem>();
            if(routerMaxHealth.getRouterType() == 3) {
                if(beacons.Length == 0) {
                    gunSounds.PlayOneShot(destroyed);
                    Destroy(router);
                } else tierIIIHub.Add(router);
            }
        }
        if(tierIIIHub.Count == 0) {
            var routerScore = PlayerPrefs.GetInt("routerScore");
            foreach (var beacon in beacons) {
                gunSounds.PlayOneShot(destroyed);
                Destroy(beacon);
                routerScore += 50;
            }
            PlayerPrefs.SetInt("routerScore", routerScore);
        }
    }

    IEnumerator overheatHUD() {
        float cooldownPercentage = Mathf.Round((((float)currentShotsFired / (float)shotsToOverheat) * 100));

        cooldownHUD.text = string.Format("Overheat: {0}%", cooldownPercentage);
        yield return new WaitForSeconds(0.25f);
    }

    IEnumerator cooldown() { // gun cooldown timer coroutine 
        Debug.Log("Weapon is in cooldown");

        isCooling = true;
        gunAnimation.SetBool("cooling", true); // trigger cooldown animation

        isCoolingDownHUD.text = "Cooling down";
        gunSounds.PlayOneShot(overheatHiss);
        
        // workaround for the delay causing you to fire before done cooling down by 0.25 secs
        yield return new WaitForSeconds(cooldownTimer - 0.25f);
        gunAnimation.SetBool("cooling", false);
        yield return new WaitForSeconds(0.25f);

        // restore "ammunition"
        currentShotsFired = 0;
        cooldownHUD.text = "Overheat: 0%";
        isCooling = false;
        isCoolingDownHUD.text = "";
    }

    void fire() { // responsible for all actions related to the raycast shooting
        var shotsFired = PlayerPrefs.GetInt("shotsFired");
        PlayerPrefs.SetInt("shotsFired", shotsFired+1);
        muzzleFlash.Play();
        gunAnimation.SetBool("firing", true);
        gunSounds.PlayOneShot(gunshot,1);
        // Vector3 fwd = cameraView.transform.forward; // if we attach a Camera to the script
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit)) {
        // if (Physics.Raycast(cameraView.transform.position, fwd, out hit, range)) { // see above Camera comment
            //Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.tag == "Router") { // check the tag of the target being shot and give an appropriate Health System
                HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
                if(routerHealth.isDead) gunSounds.PlayOneShot(destroyed);
                routerHealth.DoDamage(projectileDamage);
                crosshairEngaged.enabled = true;
                gunSounds.PlayOneShot(damaged);
                var shotsHit = PlayerPrefs.GetInt("shotsHit");
                PlayerPrefs.SetInt("shotsHit", shotsHit + 1);
            } else if (hit.transform.gameObject.tag == "Beacon") {
                HealthSystem beaconHealth = hit.transform.GetComponent<HealthSystem>();
                if(beaconHealth.isDead) gunSounds.PlayOneShot(destroyed);
                beaconHealth.DoDamage(projectileDamage);
                crosshairEngaged.enabled = true;
                gunSounds.PlayOneShot(damaged);
                var shotsHit = PlayerPrefs.GetInt("shotsHit");
                PlayerPrefs.SetInt("shotsHit", shotsHit + 1);
            } else {
                //Debug.Log("Didn't shoot router!");
                GameObject impactHit = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal)); // make an impact particle system
                Destroy(impactHit, 3f); // clear old impact sites after 3 seconds for performance
                crosshairEngaged.enabled = false;
            }
        }
    }
}
