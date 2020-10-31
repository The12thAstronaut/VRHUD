using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class EyeTracking_planes : MonoBehaviour, IGazeFocusable
{
    public Color HighlightColor = Color.red;
    public float AnimationTime = 0.1f;
    
    //public GameObject target;

    private Renderer _renderer;
    private Color _originalColor;
    private Color _targetColor;
    
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

    // Update is called once per frame
    void Update()
    {
        _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
        /*
        //This lerp will fade the color of the object
        if (_renderer.material.HasProperty(Shader.PropertyToID("_BaseColor"))) // new rendering pipeline (lightweight, hd, universal...)
        {
            _renderer.material.SetColor("_BaseColor", Color.Lerp(_renderer.material.GetColor("_BaseColor"), _targetColor, Time.deltaTime * (1 / AnimationTime)));
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        
        //only does this method
        else // old standard rendering pipline
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
            Debug.Log("inside Else");
        }
        */
    }
}
