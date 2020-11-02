using Leap.Unity;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This simple script changes the color of an InteractionBehaviour as
/// a function of its distance to the palm of the closest hand that is
/// hovering nearby.
/// </summary>
[AddComponentMenu("")]
[RequireComponent(typeof(InteractionBehaviour))]

public class Gesture_triggerPlane : MonoBehaviour
{
    [Tooltip("If enabled, the object will lerp to its hoverColor when a hand is nearby.")]
    public bool useHover = true;

    [Tooltip("If enabled, the object will use its primaryHoverColor when the primary hover of an InteractionHand.")]
    public bool usePrimaryHover = false;
    
    public GameObject target;
    
    [Header("InteractionBehaviour Colors")]
    public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1F);
    public Color suspendedColor = Color.red;
    public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7F);
    public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F);

    [Header("InteractionButton Colors")]
    [Tooltip("This color only applies if the object is an InteractionButton or InteractionSlider.")]
    public Color pressedColor = Color.white;

    private Material _material;

    private InteractionBehaviour _intObj;
    
    // Start is called before the first frame update
    void Start()
    {
        _intObj = target.GetComponent<InteractionBehaviour>();
        Renderer renderer = GetComponent<Renderer>();            //make target black and shows colors. but only R is yellow. L max is green
        if (renderer == null) {
            renderer = GetComponentInChildren<Renderer>();
        }
        if (renderer != null) {
            _material = renderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log("Hello");
    }
}
