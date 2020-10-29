using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze_triggerPlane : MonoBehaviour
{
    [SerializeField] private string selectableTag =  "Selectable";

    public Camera cam;
    public GameObject target;
    public GameObject Trigger_R;
    public GameObject Trigger_L;

    private float d = 1f;
    private Transform _selection;
    private Color m_default = Color.white;
    private Color m_select = Color.yellow;

    bool TriggerActive_R = false;
    bool TriggerActive_L = false;
    bool hideGestureMode = false;
    bool showGestureMode = false;
    private float timeStart =0;
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
            //TriggerActive_R = false;
            //TriggerActive_L = false;
        }
        
        
        Debug.DrawLine(cam.transform.position, cam.transform.position+cam.transform.forward * 10, Color.cyan);//RayCast line

        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            var selection = hit.transform;
            //trigger R
            if (hit.collider.gameObject.name == "Trigger_R" ){
                _selection = selection;
                TriggerActive_R = true;
                Trigger_R.GetComponent<Renderer>().material.color = Color.yellow;
                timeStart = 0;
            }
        
            //trigger L
            if (hit.collider.gameObject.name == "Trigger_L"){
                _selection = selection;
                TriggerActive_L = true;
                Trigger_L.GetComponent<Renderer>().material.color = Color.red;
                timeStart = 0;

            }
            

        }

        
        //Hide gesture: R ->L
        if(TriggerActive_R && !showGestureMode){
            timeStart +=Time.deltaTime;
            hideGestureMode = true;
            if(TriggerActive_L && timeStart <=2){           //checking to see if the left trigger is hit in 2 seconds
                target.SetActive(false);
                Debug.Log("Trigger L hit within 2 second");
                TriggerActive_R = false;
                TriggerActive_L = false;
                hideGestureMode = false;
            }
            
            
        }
        //show gesture: L ->R
        if(TriggerActive_L && !hideGestureMode){
            timeStart +=Time.deltaTime;
            showGestureMode = true;
            if(TriggerActive_R && timeStart <=2){           //checking to see if the left trigger is hit in 2 seconds
                target.SetActive(true);
                Debug.Log("Trigger R hit within 2 second");
                TriggerActive_R = false;
                TriggerActive_L = false;
                showGestureMode = false;
            }
           
        }
        
        if(timeStart > 2){
            TriggerActive_R = false;
            TriggerActive_L = false;
            hideGestureMode = false;
            showGestureMode = false;
            timeStart = 0;
            Debug.Log("Time Reset");
        }
        
    }
}
