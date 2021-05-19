using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideMesh : MonoBehaviour
{

    public GameObject[] triggers;
    public bool MeshToggle;

    // Start is called before the first frame update
    void Start()
    {
        MeshToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If "H" is pressed, disable all trigger mesh renderers
        if(Input.GetKeyDown(KeyCode.H))
        {
            for(int i = 0; i<triggers.Length; i++)
            {
                triggers[i].GetComponent<MeshRenderer>().enabled = MeshToggle;
            }
            //Flip value of mesh toggle boolean
            MeshToggle = !MeshToggle;
        }
    }
}
