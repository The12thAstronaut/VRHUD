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
        if(Input.GetKeyDown(KeyCode.Y))
        {
              SceneManager.LoadScene("moonScene_Eyetracking", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
              SceneManager.LoadScene("moonScene_Gaze", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
              SceneManager.LoadScene("moonScene_Gesture", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
              SceneManager.LoadScene("moonScene_Voice", LoadSceneMode.Single);
        }
    }
}
