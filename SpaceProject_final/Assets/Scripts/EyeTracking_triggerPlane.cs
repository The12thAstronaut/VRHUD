using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;
using Tobii.G2OM;
using Microsoft.MixedReality.Toolkit.UI;

public class EyeTracking_triggerPlane : MonoBehaviour, IGazeFocusable
{
    public GameObject menu;
    public GameObject Trigger_R;
    public GameObject Trigger_L;
    public GameObject Trigger_U;
    public GameObject Trigger_D;
    public Interactable ScrollDownButton;
    public Interactable ScrollUpButton;
   

    private float AnimationTime = 0.1f;
    
    private Renderer _renderer;
    private Color _originalColor = Color.white;
    private Color HighlightColor = Color.yellow;
    private Color _targetColor;

    bool TriggerActive_R = false;
    bool TriggerActive_L = false;
    bool TriggerActive_U = false;
    bool TriggerActive_D = false;
    bool hideGestureMode = false;
    bool showGestureMode = false;
    bool scrollDownGestureMode = false;
    bool scrollUpGestureMode = false;

    
    private float timeStart =0;
    
    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus){
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus){
            _targetColor = HighlightColor;
            
        }
        //If this object lost focus, fade the object's color to it's original color
        else{
             _targetColor = _originalColor;
        }
    }  
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _targetColor = _originalColor;
    }
    
    void Update()
    {
        //Debug.Log("TobiiXR.FocusedObjects: " + TobiiXR.FocusedObjects.GameObject);
        // Check whether TobiiXR has any focused objects.
        if (TobiiXR.FocusedObjects.Count > 0)
        {
            var focusedGameObject = TobiiXR.FocusedObjects[0].GameObject.name;

            // Do something with the focused game object
            Debug.Log("Hit object: " + focusedGameObject);
            
            //trigger R
            if(focusedGameObject == "Trigger_R"){
                Debug.Log(" Trigger_R !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_R.GetComponent<Renderer>().material.color = Color.yellow;
                TriggerActive_R = true;
                timeStart = 0;
            }
            
            //trigger L
            if(focusedGameObject == "Trigger_L"){
                Debug.Log(" Trigger_L !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_L.GetComponent<Renderer>().material.color = Color.red;
                TriggerActive_L = true;
                timeStart = 0;
            }

            //trigger U
            if(focusedGameObject == "Trigger_U"){
                Debug.Log(" Trigger_U !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_U.GetComponent<Renderer>().material.color = Color.yellow;
                TriggerActive_U = true;
                timeStart = 0;
            }
            
            //trigger D
            if(focusedGameObject == "Trigger_D"){
                Debug.Log(" Trigger_D !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_D.GetComponent<Renderer>().material.color = Color.red;
                TriggerActive_D = true;
                timeStart = 0;
            }
            
        }


        
        //Hide gesture: R ->L
        if(TriggerActive_R && !showGestureMode){
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
        


        //_renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
    }
}
