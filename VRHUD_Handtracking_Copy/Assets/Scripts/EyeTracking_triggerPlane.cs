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
    
    private Renderer Trigger_R_renderer;
    private Renderer Trigger_L_renderer;
    private Renderer Trigger_U_renderer;
    private Renderer Trigger_D_renderer;
    private Color Trigger_R_originalColor = Color.white;
    private Color Trigger_L_originalColor = Color.white;
    private Color Trigger_U_originalColor = Color.white;
    private Color Trigger_D_originalColor = Color.white;
    private Color HighlightColor = Color.yellow;
    private Color Trigger_R_targetColor;
    private Color Trigger_L_targetColor;
    private Color Trigger_U_targetColor;
    private Color Trigger_D_targetColor;

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
            Trigger_R_targetColor = HighlightColor;
            Trigger_L_targetColor = HighlightColor;
            Trigger_U_targetColor = HighlightColor;
            Trigger_D_targetColor = HighlightColor;
            
        }
        //If this object lost focus, fade the object's color to it's original color
        else{
            Trigger_R_targetColor = Trigger_R_originalColor;
            Trigger_L_targetColor = Trigger_L_originalColor;
            Trigger_U_targetColor = Trigger_U_originalColor;
            Trigger_D_targetColor = Trigger_D_originalColor;
        }
    }  
    
    
    // Start is called before the first frame update
    private void Start()
    {
        Trigger_R_renderer = Trigger_R.GetComponent<Renderer>();
        Trigger_L_renderer = Trigger_L.GetComponent<Renderer>();
        Trigger_U_renderer = Trigger_U.GetComponent<Renderer>();
        Trigger_D_renderer = Trigger_D.GetComponent<Renderer>();
        Trigger_R_originalColor = Trigger_R_renderer.material.color;
        Trigger_L_originalColor = Trigger_L_renderer.material.color;
        Trigger_U_originalColor = Trigger_U_renderer.material.color;
        Trigger_D_originalColor = Trigger_D_renderer.material.color;
        Trigger_R_targetColor = Trigger_R_originalColor;
        Trigger_L_targetColor = Trigger_L_originalColor;
        Trigger_U_targetColor = Trigger_U_originalColor;
        Trigger_D_targetColor = Trigger_D_originalColor;
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
                Trigger_R.GetComponent<Renderer>().material.color = HighlightColor;
                TriggerActive_R = true;
                timeStart = 0;
            }
            
            //trigger L
            if(focusedGameObject == "Trigger_L"){
                Debug.Log(" Trigger_L !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_L.GetComponent<Renderer>().material.color = HighlightColor;
                TriggerActive_L = true;
                timeStart = 0;
            }
           
            //trigger U
            if(focusedGameObject == "Trigger_U"){
                Debug.Log(" Trigger_U !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_U.GetComponent<Renderer>().material.color = HighlightColor;
                TriggerActive_U = true;
                timeStart = 0;
            }
            
            //trigger D
            if(focusedGameObject == "Trigger_D"){
                Debug.Log(" Trigger_D !!!!!!!!!!!!!!!!!!!!!!!!!!"); 
                Trigger_D.GetComponent<Renderer>().material.color = HighlightColor;
                TriggerActive_D = true;
                timeStart = 0;
            }
            
            /*
            if(focusedGameObject != "Trigger_R"){
                Trigger_R.GetComponent<Renderer>().material.color = _originalColor;
            }
            
            if(focusedGameObject != "Trigger_L"){
                Trigger_L.GetComponent<Renderer>().material.color = _originalColor;
            }
            
            if(focusedGameObject != "Trigger_U"){
                Trigger_U.GetComponent<Renderer>().material.color = _originalColor;
            }
            if(focusedGameObject != "Trigger_D"){
                Trigger_D.GetComponent<Renderer>().material.color = _originalColor;
            }
            */
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
        
        

        Trigger_R_renderer.material.color = Color.Lerp(Trigger_R_renderer.material.color, Trigger_R_targetColor, Time.deltaTime * (1 / AnimationTime));
        Trigger_L_renderer.material.color = Color.Lerp(Trigger_L_renderer.material.color, Trigger_L_targetColor, Time.deltaTime * (1 / AnimationTime));
        Trigger_U_renderer.material.color = Color.Lerp(Trigger_U_renderer.material.color, Trigger_U_targetColor, Time.deltaTime * (1 / AnimationTime));
        Trigger_D_renderer.material.color = Color.Lerp(Trigger_D_renderer.material.color, Trigger_D_targetColor, Time.deltaTime * (1 / AnimationTime));
    }
}
