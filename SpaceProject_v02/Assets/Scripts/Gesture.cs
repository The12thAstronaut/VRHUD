using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Gesture : MonoBehaviour
{
    // Start is called before the first frame update
     Controller controller;
     float HandPalmPitch;
     float HandPalmYaw;
     float HandPalmRoll;
     float HandWristRot;
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        controller = new Controller ();
        Frame frame = controller.Frame ();
        List<Hand> hands= frame.Hands;

        if (frame.Hands.Count == 0) return;

        Hand fristHand = hands[0];
        HandPalmPitch = hands [0].PalmNormal.Pitch;
        HandPalmRoll = hands [0].PalmNormal.Roll;
        HandPalmYaw = hands [0].PalmNormal.Yaw;
        HandWristRot = hands [0].WristPosition.Pitch;

        
        Debug.Log ("Pitch :" + HandPalmPitch);
        Debug.Log ("Roll :" + HandPalmRoll);
        Debug.Log ("Yaw :" + HandPalmYaw);

        if (HandPalmYaw  * Mathf.Rad2Deg > 0 && HandPalmYaw * Mathf.Rad2Deg < 90 )
        {
            transform.Translate (new Vector3(0, 0, -1 * Time.deltaTime));
        }
        else if (HandPalmYaw * Mathf.Rad2Deg > -90 && HandPalmYaw * Mathf.Rad2Deg < 180){
            transform.Translate (new Vector3(0, 0, 1 * Time.deltaTime));
        }
       

    }
}
