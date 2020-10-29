using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_test : MonoBehaviour
{
    
    public GameObject target;

    
    private Color m_oldColor = Color.white;

    private void OnTriggerStay()
    {
        Renderer render = target.GetComponent<Renderer>();
        //m_oldColor = render.material.color;
        
        render.material.color = Color.green;
        //Debug.Log("colided");
        
    }
    /*
    void OnTriggerExit(Collider other)
    {
        Renderer render = target.GetComponent<Renderer>();
        render.material.color = m_oldColor;
        //Debug.Log("not colided");
    }
    */

}
