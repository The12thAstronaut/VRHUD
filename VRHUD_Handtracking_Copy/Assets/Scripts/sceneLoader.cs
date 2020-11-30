using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{

    // public string commandPath = "C:/Users/nmchenry1/Desktop/Build_Files/Project_Final/commands/commands.csv";
    public string commandPath = "C:/Users/kdy7991/Desktop/Build_Files/Project_Final/commands/commands.csv";
    // public string prjFinalPath = "C:/Users/nmchenry1/Desktop/Build_Files/Project_Final/VRHUDProject_Final.exe";
    public string prjFinalPath = "C:/Users/kdy7991/Desktop/Build_Files/Project_Final/VRHUDProject_Final.exe";

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
            addSceneCommand("moonScene_Gaze", @commandPath);
            Process.Start(@prjFinalPath);
            Application.Quit();
        }

        //If the top side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.U))
        {
            addSceneCommand("moonScene_Eyetracking", @commandPath);
            Process.Start(@prjFinalPath);
            Application.Quit();
        }

        //If the right side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.I))
        {
            addSceneCommand("moonScene_Voice", @commandPath);
            Process.Start(@prjFinalPath);
            Application.Quit();
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O))
        {
            addSceneCommand("moonScene_Gesture", @commandPath);
            Process.Start(@prjFinalPath);
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("moonScene_PopUpWindow", LoadSceneMode.Single);
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("PracticeScene_PopUpWindow", LoadSceneMode.Single);
            Application.Quit();
        }

        //Escape Button
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public static void addSceneCommand(string sceneNameString, string filepath)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, true))
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

// 