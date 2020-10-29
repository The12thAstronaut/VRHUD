using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class GestureControl_tirgger : MonoBehaviour
{
    [SerializeField] private string selectableTag =  "Selectable";
    //[SerializeField] private Material leapRight;
    //[SerializeField] private Material leapLeft;

    public Camera cam;
    public GameObject trigger_R;
    public GameObject trigger_L;
    public GameObject trigger_U;
    public GameObject trigger_D;
    //public GameObject trigger_R_support;

    private float d = 1f;
    private Transform _selection;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Color m_oldColor = Color.white;

    // Update is called once per frame
    private void Update()
    {
        // if it's not selected, then have default material
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material.color = m_oldColor;
            
            _selection = null;
        }
        
        var gazeRay = cam.transform.forward;

        
        //var origin = cam.transform.position; //fishy

        //origin = origin + d*gazeRay; //fishy
        //Debug.DrawLine(origin, gazeRay * 10, Color.cyan);//RayCast line
        Debug.DrawLine(cam.transform.position, cam.transform.position+cam.transform.forward * 10, Color.cyan);//RayCast line


        //triggers position
        trigger_R.transform.position = cam.transform.position + .65f*gazeRay + .4f*d*cam.transform.right - .1f*d*cam.transform.up;
        trigger_L.transform.position = cam.transform.position + .65f*gazeRay - .4f*d*cam.transform.right- .1f*d*cam.transform.up;
        trigger_U.transform.position = cam.transform.position + .65f*gazeRay + .3f*d*cam.transform.up;
        trigger_D.transform.position = cam.transform.position + .7f*gazeRay - .5f*d*cam.transform.up;




        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) //Mathf.Infinity
        {
            var selection = hit.transform;
            
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.color = Color.yellow;
                }

                _selection = selection;
            }
            
        }
    }
}
