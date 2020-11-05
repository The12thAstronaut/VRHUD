using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class gesture_triggerPlane : MonoBehaviour
{
   //public Camera cam;
    //public GameObject target;
    public GameObject menu;
    public GameObject Trigger_R;
    public GameObject Trigger_L;
    public GameObject Trigger_U;
    public GameObject Trigger_D;
    public Interactable ScrollDownButton;
    public Interactable ScrollUpButton;


    private float d = 1f;
    private Transform _selection;
    private Color m_default = Color.white;
    
    private Color m_selected = new Color(1f,1f,0f, 1f);

    bool TriggerActive_R = false;
    bool TriggerActive_L = false;
    bool TriggerActive_U = false;
    bool TriggerActive_D = false;
    bool hideGestureMode = false;
    bool showGestureMode = false;
    bool scrollDownGestureMode = false;
    bool scrollUpGestureMode = false;
    private float timeStart =0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        
        
        //Debug.DrawLine(cam.transform.position, cam.transform.position+cam.transform.forward * 10, Color.cyan);//RayCast line

        //RaycastHit hit;
        
        if (Trigger_R.GetComponent<Renderer>().material.color == m_selected)
        {
            TriggerActive_R = true;
            timeStart = 0;
            Debug.Log("Trigger_R is selected");
        }

        if (Trigger_L.GetComponent<Renderer>().material.color == m_selected)
        {
            TriggerActive_L = true;
            timeStart = 0;
        }

        if (Trigger_U.GetComponent<Renderer>().material.color == m_selected)
        {
            TriggerActive_U = true;
            timeStart = 0;
        }

        if (Trigger_D.GetComponent<Renderer>().material.color == m_selected)
        {
            TriggerActive_D = true;
            timeStart = 0;
        }
        
        //Hide gesture: R ->L
        if(TriggerActive_R && !showGestureMode){
            Debug.Log("IN HIDE GESTURE MODE");
            timeStart +=Time.deltaTime;
            hideGestureMode = true;
            if(TriggerActive_L && timeStart <=1){           //checking to see if the left trigger is hit in 2 seconds
                menu.SetActive(false);
                Debug.Log("Trigger L hit within 1 second");
                TriggerActive_R = false;
                TriggerActive_L = false;
                hideGestureMode = false;
            }
            
            
        }
        //show gesture: L ->R
        if(TriggerActive_L && !hideGestureMode){
            timeStart +=Time.deltaTime;
            showGestureMode = true;
            if(TriggerActive_R && timeStart <=1){           //checking to see if the left trigger is hit in 2 seconds
                menu.SetActive(true);
                Debug.Log("Trigger R hit within 1 second");
                TriggerActive_R = false;
                TriggerActive_L = false;
                showGestureMode = false;
            }
           
        }
        
        //Scroll Down Gesture: U ->D, testing with space bar first
        if(TriggerActive_U && !scrollUpGestureMode){
            timeStart +=Time.deltaTime;
            scrollDownGestureMode = true;
            
            if(TriggerActive_D && timeStart <=1){           //checking to see if the left trigger is hit in 2 seconds
                //target.SetActive(false);
                Debug.Log("Trigger D hit within 1 second");
                TriggerActive_U = false;
                TriggerActive_D = false;
                scrollDownGestureMode = false;
                ScrollDownButton.TriggerOnClick();
            }
            
            
        }
        

        //Scroll Up Gesture: D ->U, testing with "U" key
        if(TriggerActive_D && !scrollDownGestureMode){
            timeStart +=Time.deltaTime;
            scrollUpGestureMode = true;
            if(TriggerActive_U && timeStart <=1){           //checking to see if the left trigger is hit in 2 seconds
                //target.SetActive(true);
                Debug.Log("Trigger U hit within 1 second");
                TriggerActive_U = false;
                TriggerActive_D = false;
                scrollUpGestureMode = false;
                ScrollUpButton.TriggerOnClick();
            }
           
        }
        
        if(timeStart > 1){
            TriggerActive_R = false;
            TriggerActive_L = false;
            TriggerActive_U = false;
            TriggerActive_D = false;
            hideGestureMode = false;
            showGestureMode = false;
            scrollDownGestureMode = false;
            scrollUpGestureMode = false;
            timeStart = 0;
            Debug.Log("Time Reset");
        }
        
    }
}
