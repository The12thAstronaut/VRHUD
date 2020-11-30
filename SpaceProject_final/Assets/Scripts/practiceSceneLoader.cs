using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class practiceSceneLoader : MonoBehaviour
{

    public string[] scenes;
    public int practiceSceneIndex;

    void Awake()
    {   
        scenes = new string[] {"PracticeScene_Gaze", "PracticeScene_Eyetracking", "PracticeScene_Voice", "PracticeScene_Gesture", "PracticeScene_PopUpWindow"};

        //Makes sure that all the data is together
        DontDestroyOnLoad(this.gameObject);
        practiceSceneIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Switch between Gaze, eyetracking, voice, gesture, and wrist practice scenes when LeftAlt is pressed
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            /*
            if(scenes[practiceSceneIndex] == "PracticeScene_PopUpWindow")
            {
                Process.Start(@"C:/Users/kdy7991/Desktop/Build_Files/Practice_popUpWindow/VRHUD_Handtracking.exe");
                Application.Quit();
            }
            */
            //else{
                SceneManager.LoadScene(scenes[practiceSceneIndex], LoadSceneMode.Single);
            //}
            practiceSceneIndex++;
        }
    }
}
