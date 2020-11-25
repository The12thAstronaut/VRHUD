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
            addSceneCommand("moonScene_Gaze", "commands/commands.csv");
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final\VRHUDProject_Final.exe");
        }

        //If the top side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.U))
        {
            addSceneCommand("moonScene_Eyetracking", "commands/commands.csv");
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final\VRHUDProject_Final.exe");
        }

        //If the right side of the VIVE left/right controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.I))
        {
            addSceneCommand("moonScene_Voice", "commands/commands.csv");
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final\VRHUDProject_Final.exe");
        }

        //If the bottom side of the VIVE left controller trackpad is pressed, load scene
        if(Input.GetKeyDown(KeyCode.O))
        {
            addSceneCommand("moonScene_Gesture", "commands/commands.csv");
            Process.Start(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final\VRHUDProject_Final.exe");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("moonScene_PopUpWindow", LoadSceneMode.Single);
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
