﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
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
            SceneManager.LoadScene("moonScene_PopUpWindow", LoadSceneMode.Single);
        }

        //Escape Button
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

}

// 