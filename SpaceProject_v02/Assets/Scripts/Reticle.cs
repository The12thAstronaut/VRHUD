using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt (cam.transform.position);
        transform.Rotate (.0f, 180f, .0f);
        transform.position = cam.transform.position + 1f*cam.transform.forward;
    }
}
