using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{
    [SerializeField] private string selectableTag =  "Selectable";
    
    public Camera cam;
    private float d = 1f;
    private Transform _selection;
    private Color m_default = Color.white;
    private Color m_select = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        // if it's not selected, then have default material
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material.color = m_default;
            
            _selection = null;
        }
        
        var gazeRay = cam.transform.forward;

        
        Debug.DrawLine(cam.transform.position, cam.transform.position+cam.transform.forward * 10, Color.cyan);//RayCast line

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) //Mathf.Infinity
        {
            var selection = hit.transform;
            
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.color = m_select;
                }

                _selection = selection;
            }
            
        }
    }
}
