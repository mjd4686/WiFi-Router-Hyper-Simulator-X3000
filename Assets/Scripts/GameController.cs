using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] routers;

    public Text timerLabel;
    private float time;

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
        else{
            time += Time.deltaTime;
            var minutes = time / 60;
            var seconds = time % 60;
            //update the label value
            timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
        }
    }
}
