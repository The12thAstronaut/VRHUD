using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_D : MonoBehaviour
{
    [SerializeField]
    //private string selectableTag =  "Selectable";
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Color m_oldColor = Color.white;
    private Color m_newColor = Color.cyan;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Renderer render = target.GetComponent<Renderer>();
        m_oldColor = render.material.color;
        render.material.color = m_newColor;
        
        
    }

    void OnTriggerExit(Collider other)
    {
        Renderer render = target.GetComponent<Renderer>();
        render.material.color = m_oldColor;
    }
}
