﻿using System.Collections;
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
    public float range = 200f;

    public ParticleSystem muzzleFlash;
    public GameObject impact;

    public Text timerLabel;
    private float time;

    // public Camera cameraView;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine ("Countdown", 60f);
    }

    private IEnumerator Countdown (float time) {
        while (time >= 0f) {
            if (routers.Length == 0) {
                Debug.Log("All routers destroyed!");
            } else {
                var minutes = (time / 60) - 1;
                var seconds = time % 60;
                //update the label value
                timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
            }
            Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Countdown Complete!");
    }

    // Update is called once per frame
    void Update() {
        // Fire if mouse held
        if (Input.GetButton("Fire1") && Time.time > nextShot){
            nextShot = Time.time + rateOfFire;
            fire();
        }
        
        //routers & beacons
        routers = GameObject.FindGameObjectsWithTag("Router");
        beacons = GameObject.FindGameObjectsWithTag("Beacon");

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

    void fire() {
        muzzleFlash.Play();
        // Vector3 fwd = cameraView.transform.forward;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit)) {
        // if (Physics.Raycast(cameraView.transform.position, fwd, out hit, range)) {
            Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.tag == "Router") {
                HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
                routerHealth.DoDamage(projectileDamage);
            } else if (hit.transform.gameObject.tag == "Beacon") {
                HealthSystem beaconHealth = hit.transform.GetComponent<HealthSystem>();
                beaconHealth.DoDamage(projectileDamage);
            } else {
                Debug.Log("Didn't shoot router!");
                GameObject impactHit = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactHit, 3f);
            }
        }
    }
}
