using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string selectableTag =  "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    public Camera cam;
    public GameObject UI;

    private float d = 1f;
    private Transform _selection;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }
        
        var gazeRay = cam.transform.forward;
        var origin = UI.transform.position;

        origin = cam.transform.position + d*gazeRay;
        
        RaycastHit hit;
        if (Physics.Raycast(origin, gazeRay, out hit)) //Mathf.Infinity
        {
            var selection = hit.transform;
            
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
            
        }
    }
}
