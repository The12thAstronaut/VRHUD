using System.Collections;
using System.Diagnostics;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Create a commands directory, if it doesn't already exist
        Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "/commands");
    }

    // Update is called once per frame
    void Update()
    {
        //If the left side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.Y))
        {
            //addSceneCommand("moonScene_Gaze", "commands/commands.csv");
            SceneManager.LoadScene("moonScene_Gaze", LoadSceneMode.Single);
        }

        //If the top side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.U))
        {
            //addSceneCommand("moonScene_Eyetracking", "commands/commands.csv");
            SceneManager.LoadScene("moonScene_Eyetracking", LoadSceneMode.Single);
        }

        //If the right side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.I))
        {
            //addSceneCommand("moonScene_Voice", "commands/commands.csv");
            SceneManager.LoadScene("moonScene_Voice", LoadSceneMode.Single);
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O))
        {
            //addSceneCommand("moonScene_Gesture", "commands/commands.csv");
            SceneManager.LoadScene("moonScene_Gesture", LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            //addSceneCommand("moonScene_PopUpWindow", "commands/commands.csv");
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\PopUp_Window\VRHUD_Handtracking.exe");
            // Process.Start(@"C:\Users\nmchenry1\Desktop\Build_Files\PopUp_Window\VRHUD_Handtracking.exe");
            Application.Quit();
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

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public static void addSceneCommand(string sceneNameString, string filepath)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                //Save data as a new line in CSV file
                file.WriteLine(sceneNameString + ",");
            }
        }
        catch(Exception ex)
        {
            throw new ApplicationException("This program did an oopsie :", ex);
        }
    }
}

