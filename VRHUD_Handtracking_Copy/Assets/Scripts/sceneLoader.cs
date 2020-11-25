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
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("moonScene_Gaze", LoadSceneMode.Single);
        }

        //If the top side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("moonScene_Eyetracking", LoadSceneMode.Single);
        }

        //If the right side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.I))
        {
            // Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final\VRHUDProject_Final.exe");
            // :(
            
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("moonScene_Gesture", LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("moonScene_PopUpWindow", LoadSceneMode.Single);
        }



    }
}
