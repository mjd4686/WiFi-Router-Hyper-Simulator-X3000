using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] routers;
    public GameObject[] beacons;
    private float projectileDamage = 30f;
    private float rateOfFire = 0.2f;
    private float nextShot = 0f;

    public ParticleSystem muzzleFlash;
    public GameObject impact;

    public Text timerLabel;
    private float time;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        // Fire if mouse clicked
        // if (Input.GetMouseButtonDown(0)) {
        //     muzzleFlash.Play();
        //     Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //     RaycastHit hit;

        //     if (Physics.Raycast(transform.position, fwd, out hit)) {
        //         Debug.Log(hit.collider.name);
        //         if (hit.transform.gameObject.tag == "Router") {
        //             HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
        //             routerHealth.DoDamage(projectileDamage);
        //         } else if (hit.transform.gameObject.tag == "Beacon") {
        //             HealthSystem beaconHealth = hit.transform.GetComponent<HealthSystem>();
        //             beaconHealth.DoDamage(projectileDamage);
        //         } else {
        //             Debug.Log("Didn't shoot router!");
        //             Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        //         }
        //     }
        
        // }

        // Fire if mouse held
        if (Input.GetButton("Fire1") && Time.time > nextShot){
            nextShot = Time.time + rateOfFire;
            fire();
        }
        
        //routers & beacons
        routers = GameObject.FindGameObjectsWithTag("Router");
        beacons = GameObject.FindGameObjectsWithTag("Beacon");

        if (routers.Length == 0) {
            Debug.Log("All routers destroyed!");
        }
        else {
            time += Time.deltaTime;
            var minutes = time / 60;
            var seconds = time % 60;
            //update the label value
            timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
        }
    }

    void fire() {
        muzzleFlash.Play();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit)) {
            Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.tag == "Router") {
                HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
                routerHealth.DoDamage(projectileDamage);
            } else if (hit.transform.gameObject.tag == "Beacon") {
                HealthSystem beaconHealth = hit.transform.GetComponent<HealthSystem>();
                beaconHealth.DoDamage(projectileDamage);
            } else {
                Debug.Log("Didn't shoot router!");
                Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
