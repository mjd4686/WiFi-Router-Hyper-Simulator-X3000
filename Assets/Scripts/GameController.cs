using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] routers;
    public GameObject[] beacons;

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
    private float time;

    // public Camera cameraView; // if using camera

    // Start is called before the first frame update
    void Start() {
        StartCoroutine ("Countdown", 60f);
        currentShotsFired  = 0;
    }

    private IEnumerator Countdown (float time) { // coroutine responsible for game timer, counts down and displays in the canvas HUD. Ends if all routers are destroyed.
        while (time >= 0f) {
            if (routers.Length == 0) {
                Debug.Log("All routers destroyed!");
            } else {
                var minutes = (time / 60) - 1;
                var seconds = time % 60;
                //update the label value
                timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
            }
            //Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Countdown Complete!");
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
                if(beacons.Length == 0) Destroy(router);
                else tierIIIHub.Add(router);
            }
        }
        if(tierIIIHub.Count == 0) {
            foreach(var beacon in beacons) {
                Destroy(beacon);
            }
        }

        // Old timer code (counts up)
        // if (routers.Length == 0) {
        //     Debug.Log("All routers destroyed!");
        // }
        // else {
        //     time += Time.deltaTime;
        //     var minutes = time / 60;
        //     var seconds = time % 60;
        //     //update the label value
        //     timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
        // }
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
        muzzleFlash.Play();
        gunAnimation.SetBool("firing", true);
        // Vector3 fwd = cameraView.transform.forward; // if we attach a Camera to the script
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit)) {
        // if (Physics.Raycast(cameraView.transform.position, fwd, out hit, range)) { // see above Camera comment
            //Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.tag == "Router") { // check the tag of the target being shot and give an appropriate Health System
                HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
                routerHealth.DoDamage(projectileDamage);
            } else if (hit.transform.gameObject.tag == "Beacon") {
                HealthSystem beaconHealth = hit.transform.GetComponent<HealthSystem>();
                beaconHealth.DoDamage(projectileDamage);
            } else {
                //Debug.Log("Didn't shoot router!");
                GameObject impactHit = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal)); // make an impact particle system
                Destroy(impactHit, 3f); // clear old impact sites after 3 seconds for performance
            }
        }
    }
}
