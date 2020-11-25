﻿using System;
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
    private List<string> data_sceneCommand;

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

    public List<string> data_participantID;
    public List<string> data_currentTime;
    public List<string> data_timeDifference;
    public List<string> data_sceneName;
    public List<string> data_trialNumber;

    public string[] csvFiles;
    public string recentCSV;

    private float timeOffset;
    private int trialOffset;


    void Awake()
    {   
        //Makes sure that all the data is together
        DontDestroyOnLoad(this.gameObject);

        //Read most recent CSV file data
            //Search for CSV files within project directory
            csvFiles = System.IO.Directory.GetFiles(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final", "*.csv");

            //read the csv file from the other project
            var files = new DirectoryInfo(@"C:\Users\kdy7991\Desktop\Build_Files\Project_Final").GetFiles("*.csv");
            DateTime lastModified = DateTime.MinValue;
            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastModified)
                {
                    lastModified = file.LastWriteTime;
                    recentCSV = file.Name;
                }
            }

            //Initialize data_participantID array's first and second element to null
            data_participantID.Add("null");
            data_participantID.Add("null");

            //Call the readCSV function and use the most recent CSV as an input
            readCSV(recentCSV);
            //set participant id based on last element of data_participantID array
            participantID = data_participantID[data_participantID.Count - 1];

            //Figure out time offset based on most recent time
            timeOffset = (float) Convert.ToDouble(data_currentTime[data_currentTime.Count - 1]);

            //Create trial number offset
            trialOffset = (int) Convert.ToDouble(data_trialNumber[data_trialNumber.Count - 1]);

             //Read CSV file data, hardcoded option
            // string CSVPath = @"C:\Users\nmchenry1\Documents\GitHub\VRHUD\SpaceProject_final\12_VRHUD_Task_Time.csv";
            // readCSV(CSVPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the past time to the time offset from the CSV file data
        pastTime = timeOffset;
        //Don't need the -1 since we aren't using the start scene
        trialNumber = trialOffset;
        // InputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

        //Loads the scene specified in the commands.csv
        // loadCommandedScene("commands/commands.csv");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time + timeOffset;
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
            //Add a header line to the csv file, but only if that file is new and a past participant number exists in the most recent CSV file
                if(data_participantID[1] != participantID)
                {
                    addRecord("participantID", "currentTimeString", "timeDifferenceString", "sceneName", "trialNumber", "C:/Users/kdy7991/Desktop/Build_Files/Project_Final/" + participantID + "_VRHUD_Task_Time.csv");
                }
            //Add data as a new line to csv file
            addRecord(participantID, currentTimeString, timeDifferenceString, sceneName, trialNumberRef, "C:/Users/kdy7991/Desktop/Build_Files/Project_Final/" + participantID + "_VRHUD_Task_Time.csv");
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
        // participantID = InputField.GetComponent<TMP_InputField>().text;
        Debug.Log("Value Changed");
    }

    //Function that reads CSV file
    public void readCSV(string csvPath)
    {
        //Read in CSV file specified by csvPath input variable
        using(var reader = new StreamReader(csvPath))
        {
            //Create a different string list for each CSV column
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();
            List<string> listD = new List<string>();
            List<string> listE = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                listA.Add(values[0]);
                listB.Add(values[1]);
                listC.Add(values[2]);
                listD.Add(values[3]);
                listE.Add(values[4]);

                data_participantID = listA;
                data_currentTime = listB;
                data_timeDifference = listC;
                data_sceneName = listD;
                data_trialNumber = listE;
            }
        }
    }

    //Function that reads the "commands.csv" CSV file and loads the last scene listed
    public void loadCommandedScene(string commandsPath)
    {
        //Read in CSV file specified by csvPath input variable
        using(var reader = new StreamReader(commandsPath))
        {
            //Create a different string list for each CSV column
            List<string> listTemp = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                listTemp.Add(values[0]);
                data_sceneCommand = listTemp;
            }
        }

        //Save last command CSV array element to string
        string lastElement = data_sceneCommand[data_sceneCommand.Count - 1];

        //Check to see if the command has been executed or not yet. If not, load the scene and add the string "Finished"
            if(lastElement != "Finished")
            {
                addFinishedCommand("commands/commands.csv");
                //Loads the scene stored in the second to last element of the data_sceneCommmand list
                Debug.Log("Loading " + lastElement + "...");
                SceneManager.LoadScene(lastElement, LoadSceneMode.Single);
            }

        //If most recent scene was the pop-up menu, open the scene commanded by the pop-up scene
            // if(data_sceneName[data_sceneName.Count - 1] == "moonScene_Eyetracking")
            // {
            //     Debug.Log("Loading Popup Scene");
            // }
        //Else do nothing
    }

    public static void addFinishedCommand(string filepath)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                //Save data as a new line in CSV file
                file.WriteLine("Finished" + ",");
            }
        }
        catch(Exception ex)
        {
            throw new ApplicationException("This program did an oopsie :", ex);
        }
    }

    //Work In Progress:
    //Read the participant ID and set it when the scene is reopened
}

//why is life so difficult lol

/*
how to be cool
A) cool sunglasses emoji
B)

yup that's an emoji pun
*/