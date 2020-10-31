using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;
using Tobii.G2OM;

public class EyeTracking_test : MonoBehaviour, IGazeFocusable
{
    public GameObject target;
    public GameObject target2;
    public GameObject Trigger_R;
    public GameObject Trigger_L;
    
    
    private float AnimationTime = 0.1f;
    
    private Renderer _renderer;
    private Color _originalColor = Color.white;
    private Color HighlightColor = Color.yellow;
    private Color _targetColor;

    bool TriggerActive_R = false;
    bool TriggerActive_L = false;
    bool hideGestureMode = false;
    bool showGestureMode = false;

    
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
            if(focusedGameObject == "Cube"){
                Debug.Log(" Target !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                target.GetComponent<Renderer>().material.color = Color.yellow;
                TriggerActive_R = true;
                timeStart = 0;
                
            }
            
            /*
            //trigger L
            if(focusedGameObject == "Trigger_L"){
                Debug.Log(" Trigger_L !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                target.GetComponent<Renderer>().material.color = Color.red;
                TriggerActive_R = true;
                timeStart = 0;
                
            }
            */
            
        }


        /*
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

        if(timeStart > 2){
            TriggerActive_R = false;
            TriggerActive_L = false;
            //TriggerActive_U = false;
            //TriggerActive_D = false;
            hideGestureMode = false;
            showGestureMode = false;
            //scrollDownGestureMode = false;
            //scrollUpGestureMode = false;
            timeStart = 0;
            Debug.Log("Time Reset");
        }
        */


        //_renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
    }
}
