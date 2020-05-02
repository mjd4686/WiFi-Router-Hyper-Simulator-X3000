using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunscript : MonoBehaviour
{
    public GameObject[] routers;
    // Start is called before the first frame update
    void Start()
    {
        
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
                    Destroy(hit. transform. gameObject);
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
    }
}
