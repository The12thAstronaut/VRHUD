using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_L : MonoBehaviour
{
    [SerializeField]
    //private string selectableTag =  "Selectable";
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color m_oldColor = Color.white;
    private Color m_newColor = new Color(1f,0.62f,0.28f, 0f);

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
