using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class logCSV : MonoBehaviour
{
    private string currentTimeString;
    public float timeDifference;
    public string timeDifferenceString;

    public float currentTime;    
    public float pastTime;
    public string participantID;

    public Scene currentScene;
    public string sceneName;
    public string oldSceneName;

    public int trialNumber;
    public string trialNumberRef;
    public TMP_InputField InputField; 

    void Awake()
    {   
        //Makes sure that all the data is together
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pastTime = 0.0f;
        //Add a header line to the csv file
        addRecord("participantID", "currentTimeString", "timeDifferenceString", "sceneName", "sceneNumber", participantID + "_VRHUD_Task_Time.csv");
        trialNumber = -1;
        InputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        int min = Mathf.FloorToInt(currentTime/60);
        int sec = Mathf.FloorToInt(currentTime%60);
        currentTimeString = min.ToString("00") + ":" + sec.ToString("00");     //Save game time in seconds as a string
       
        //Set currentScene to the active scene
        currentScene = SceneManager.GetActiveScene();
         // Retrieve the name of this scene
        sceneName = currentScene.name;

        //If the current scene isn't the same as the old scene, add one to the trial number
        if(sceneName != oldSceneName)
        {
            trialNumber++;
        }

        //Get trial number from the sceneLoader class and convert it to a string
        trialNumberRef = trialNumber.ToString();

        //Only record data if the space bar is pressed on the keyboard
        if(Input.GetKeyDown(KeyCode.Space))
        {
            timeDifference = (currentTime - pastTime);
            int min_diff = Mathf.FloorToInt(timeDifference/60);
            int sec_diff = Mathf.FloorToInt(timeDifference%60);
            timeDifferenceString = min_diff.ToString("00") + ":" + sec_diff.ToString("00");     //Save game time in seconds as a string
           
            addRecord(participantID, currentTimeString, timeDifferenceString, sceneName, trialNumberRef, participantID + "_VRHUD_Task_Time.csv");
            Debug.Log("Time: " + currentTimeString + "logged to CSV");
            pastTime = currentTime;
        }

        //Saves most recent scene name
        oldSceneName = sceneName;

    }

    public static void addRecord(string participantID,
                                 string time,
                                 string timeDifference,
                                 string sceneName,
                                 string trialNumberRef,
                                 string filepath)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                //Save data as a new line in CSV file
                file.WriteLine(participantID + ","
                                 + time + ","
                                 + timeDifference  + ","
                                 + sceneName  + ","
                                 + trialNumberRef  + ","
                                );
            }
        }
        catch(Exception ex)
        {
            throw new ApplicationException("This program did an oopsie :", ex);
        }
    }
    public void ValueChangeCheck()
    {
        participantID = InputField.GetComponent<TMP_InputField>().text;
        Debug.Log("Value Changed");
    }

    //Function that reads CSV file
    public void readCSV()
    {
        //Make string path to CSV file
        string CSVPath;
        //Read last line from CSV file
        //If most recent scene was the pop-up menu, open the scene commanded by the pop-up scene
        //Else do nothing
        
    }
}