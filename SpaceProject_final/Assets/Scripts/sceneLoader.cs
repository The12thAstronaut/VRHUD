using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If the left side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.Y)
            || (Input.GetAxis("LeftVIVETrackpadHorizontal") == -1.0) 
            || (Input.GetAxis("RightVIVETrackpadHorizontal") == -1.0)
            )
        {
            SceneManager.LoadScene("moonScene_Gaze", LoadSceneMode.Single);
        }

        //If the top side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.U)
            || (Input.GetAxis("LeftVIVETrackpadVertical") == 1.0)
            || (Input.GetAxis("RightVIVETrackpadVertical") == 1.0)
            )
        {
            SceneManager.LoadScene("moonScene_Eyetracking", LoadSceneMode.Single);
        }

        //If the right side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.I)
            || (Input.GetAxis("LeftVIVETrackpadHorizontal") == 1.0) 
            || (Input.GetAxis("RightVIVETrackpadHorizontal") == 1.0)
            )
        {
            SceneManager.LoadScene("moonScene_Gesture", LoadSceneMode.Single);
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O)
                || (Input.GetAxis("LeftVIVETrackpadVertical") == -1.0)
                || (Input.GetAxis("RightVIVETrackpadVertical") == -1.0)
            )
        {
            SceneManager.LoadScene("moonScene_Voice", LoadSceneMode.Single);
        }

        if (Input.GetAxis("LeftVRTriggerAxis") == 1.0)
        {
            Debug.Log("Left Controller Trigger Pressed");
        }
    }
}
