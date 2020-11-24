using System.Collections;
using System.Diagnostics;
using System;
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
            SceneManager.LoadScene("moonScene_Voice", LoadSceneMode.Single);
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("moonScene_Gesture", LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\PopUp_Window\VRHUD_Handtracking.exe");
            /*
            Process foo = new Process();
            foo.StartInfo.FileName = @"C:\Windows\system32\cmd.exe";
            string path = @"C:\Users\kdy7991\Desktop\VRHUD_Handtracking.exe";
            foo.StartInfo.Arguments = "start " + path;
            foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            foo.Start();
            //foo.WaitForExit();
            //int ExitCode = foo.ExitCode;
            Application.Quit();
            */
        }

    }
}

