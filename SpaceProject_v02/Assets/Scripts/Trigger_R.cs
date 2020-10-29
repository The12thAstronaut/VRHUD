using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_R : MonoBehaviour
{
    [SerializeField]
    //private string selectableTag =  "Selectable";
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color m_oldColor = Color.white;

    void OnTriggerEnter(Collider other)
    {
        Renderer render = target.GetComponent<Renderer>();
        m_oldColor = render.material.color;
        
        render.material.color = Color.green;
        //Debug.Log("colided");
        
    }
    
    void OnTriggerExit(Collider other)
    {
        Renderer render = target.GetComponent<Renderer>();
        render.material.color = m_oldColor;
        //Debug.Log("not colided");
    }
    

}
