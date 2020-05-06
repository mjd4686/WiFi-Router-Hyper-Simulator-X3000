using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] routers;
    private float projectileDamage = 30f;
    // public GameObject[] tierIRouters;
    // public GameObject[] tierIIRouters;
    // public GameObject[] tierIIIRouters;
    // public GameObject[] tierIIIBeacons;

    public Text timerLabel;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        // tierIRouters = GameObject.FindGameObjectsWithTag("Tier_1_Router");
        // tierIIRouters = GameObject.FindGameObjectsWithTag("Tier_2_Router");
        // tierIIIRouters = GameObject.FindGameObjectsWithTag("Tier_3_Router");
        // tierIIIBeacons = GameObject.FindGameObjectsWithTag("Tier_3_Beacon");

        // GameObject[][] routerList = { tierIRouters, tierIIRouters, tierIIIRouters, tierIIIBeacons };

        // for(int tier = 1; tier <= routerList.Length; tier++)
        //     foreach(var router in routerList[tier-1]) {
        //         DamageSystem newDamageSystem = new DamageSystem(tier);
        //     }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, fwd, out hit)){
                print(hit.collider.name);
                if(hit.transform.gameObject.tag == "Router"){
                    HealthSystem routerHealth = hit.transform.GetComponent<HealthSystem>();
                    routerHealth.DoDamage(projectileDamage);
                    // Destroy(hit. transform. gameObject);
                }
                else{
                    print("Didn't shoot router!");
                }
            }
        
        }

        
        //routers
        routers = GameObject.FindGameObjectsWithTag("Router");
        if(routers.Length == 0){
            print("All routers destroyed!");
        }
        else{
            time += Time.deltaTime;
            var minutes = time / 60;
            var seconds = time % 60;
            //update the label value
            timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
        }
    }
}
