using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Triggers_position : MonoBehaviour
{
    public Camera cam;
    public GameObject trigger_R;
    public GameObject trigger_L;
    public GameObject trigger_U;
    public GameObject trigger_D;
    //public GameObject trigger_R_support;

    private float d = 1f;
    private Transform _selection;
     
    void Start()
    {
        
    }


    // Update is called once per frame
    private void Update()
    {
        
        var gazeRay = cam.transform.forward;

        //triggers position
        trigger_R.transform.position = cam.transform.position + .65f*gazeRay + .4f*d*cam.transform.right - .1f*d*cam.transform.up;
        trigger_L.transform.position = cam.transform.position + .65f*gazeRay - .4f*d*cam.transform.right- .1f*d*cam.transform.up;
        trigger_U.transform.position = cam.transform.position + .65f*gazeRay + .3f*d*cam.transform.up;
        trigger_D.transform.position = cam.transform.position + .7f*gazeRay - .5f*d*cam.transform.up;

    }
    
}
