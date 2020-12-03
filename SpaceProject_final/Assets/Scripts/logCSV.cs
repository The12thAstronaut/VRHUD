﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Random=UnityEngine.Random;

public class logCSV : MonoBehaviour
{
    private string currentTimeString;
    private List<string> data_sceneCommand;

    public static System.Random ran = new System.Random();

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
    public TextMeshProUGUI debugText; 
    public GameObject debugTextCanvas;
    private bool debugTextBool;

    public List<string> data_participantID;
    public List<string> data_currentTime;
    public List<string> data_timeDifference;
    public List<string> data_sceneName;
    public List<string> data_trialNumber;
    public List<string> data_sceneOrder;

    public string[] csvFiles;
    public string recentCSV;

    private float timeOffset;
    private int trialOffset;

    public bool newFileBool;
    public string CommandsPath;

    public bool recordingFlag;
    private float startTime;
    private float endTime;
    public float recordedTime;

    private int sNameCounter;

    public string[] sceneArray;
    public string sceneFilePath;

    private int sceneLoadIndex;

    void Awake()
    {   
        
        //Makes sure that all the data is together and doesn't get destroyed between scenes
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(debugTextCanvas);

        sceneFilePath = "null";
        sceneLoadIndex = 0;

        //Set path to commands csv
        CommandsPath = "commands/commands.csv";
        
        //Loads the scene specified in the commands.csv
        loadCommandedScene(CommandsPath);

        //Read most recent CSV file data
            //Search for CSV files within project directory
            csvFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.csv");

            //Find most recent CSV file and save the string to the recentCSV variable
            var files = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory()).GetFiles("*.csv");
            DateTime lastModified = DateTime.MinValue;
            
            //Initialize recentCSV
            recentCSV = "null";
            
            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastModified)
                {
                    lastModified = file.LastWriteTime;
                    recentCSV = file.Name;
                }
            }

            //Display the value of recentCSV on the screen
            debugText.text = "recentCSV = <" + recentCSV + ">";

            //Initialize data array elements first and second elements to null so there are no index reading errors
            data_participantID.Add("null");
            data_participantID.Add("null");
            data_currentTime.Add("null");
            data_currentTime.Add("null");
            data_trialNumber.Add("null");
            data_trialNumber.Add("null");
            data_sceneName.Add("null");
            data_sceneName.Add("null");
            data_sceneOrder.Add("null");
            data_sceneOrder.Add("null");

            //Call the readCSV function and use the most recent CSV as an input, if it exists
            if(recentCSV != "null")
            {
            readCSV(recentCSV);
            }

            //Set participant id based on last element of data_participantID array, if a valid value exists
            if(data_participantID[1] != "null")
            {
                participantID = data_participantID[data_participantID.Count - 1];
            }

            //Figure out time offset based on most recent time and convert it to a float number in terms of seconds
            if(data_currentTime[1] != "null")
            {
                //Split the minutes and seconds into two separate strings, with ":" as the delimiter
                var timeStrings = data_currentTime[data_currentTime.Count - 1].Split(':');

                //Convert strings to float values and store total time in seconds within the timeOffset
                var minuteFloat = (float) Convert.ToDouble(timeStrings[0]);
                var secondFloat = (float) Convert.ToDouble(timeStrings[1]);
                timeOffset = minuteFloat * 60 + secondFloat;              
            }

            //Create trial number offset
            if(data_trialNumber[1] != "null")
            {
                trialOffset = (int) Convert.ToDouble(data_trialNumber[data_trialNumber.Count - 1]);
                trialOffset = trialOffset - 1;    //Decrement trialOffset by 1 to account for startScene
            }
        //Read CSV file data, hardcoded option
            // string CSVPath = @"C:/Users/nmchenry1/Documents/GitHub/VRHUD/SpaceProject_final/12_VRHUD_Task_Time.csv";
            // readCSV(CSVPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        recordingFlag = false;          //Initialize recordingFlag to false
        
        //By default, import the data offset values from the CSV file
        //      However, set these offset to 0 if the participant value changes
        //Set the past time to the time offset from the CSV file data
        pastTime = timeOffset;
        //Don't need the -1 since we aren't using the start scene
        trialNumber = trialOffset;

        //Hide the debug text on start
        debugTextBool = false;
        debugText.gameObject.SetActive(debugTextBool);

        //Initialize the newFileBool to false
        newFileBool = false;

        //ValueChangeCheck function called when input field value changed
        InputField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

        //Shuffle function called when editing of input field ends
        InputField.onEndEdit.AddListener(delegate {SubmitParticipantNumber(); });
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

        //If the D key is pressed, toggle the debug text
        if(Input.GetKeyDown(KeyCode.D))
        {
            debugTextBool = !debugTextBool;
            debugText.gameObject.SetActive(debugTextBool);
        }

        //Start recording if the space bar is pressed on the keyboard and the recordingFlag is false
        if(Input.GetKeyDown(KeyCode.Space) && !recordingFlag)
        {
            print("Recording Started.");        //Print message in console
            recordingFlag = true;               //Start recording data, indicated by flag 
            startTime = currentTime;  
        }

        //Finish recording time when enter is pressed and the recordingFlag is true, then log to the CSV file
        if(Input.GetKeyDown(KeyCode.Return) && recordingFlag)
        {
            print("Recording Stopped.");        //Print message in console
            recordingFlag = false;               //Stop recording data, indicated by flag 
            endTime = currentTime;
            recordedTime = endTime - startTime; //Calculate the recorded time through subtraction
          
            int min_diff = Mathf.FloorToInt(recordedTime/60);   //Convert recordedTime to minutes
            int sec_diff = Mathf.FloorToInt(recordedTime%60);   //Convert recordedTime to seconds
            timeDifferenceString = min_diff.ToString("00") + ":" + sec_diff.ToString("00");     //Save game time in seconds as a string

            sceneName = sceneCounter(sceneName);//Call function that returns the scene name with a number, without the "moonScene_"

            //Add a header line to the csv file, but only if that file is new and a past participant number exists in the most recent CSV file
            if(newFileBool)
            {
                addRecord("participantID", "currentTimeString", "timeDifferenceString", "sceneName", "trialNumber", participantID + "_VRHUD_Task_Time.csv");
                newFileBool = false;
            }
            //Add data as a new line to csv file
            addRecord(participantID, currentTimeString, timeDifferenceString, sceneName, trialNumberRef, participantID + "_VRHUD_Task_Time.csv");
            print(timeDifferenceString + " " + sceneName + " logged to CSV");
            pastTime = currentTime;
        }

        //Press tab to load the next Moon Scene based on the randomized scene order
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //Call function Read participant CSV and load next scene
            loadSceneCSV(sceneFilePath);
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
        print("Value Changed");

        //Set newFileBool to true if the input participant ID is changed
        newFileBool = true;

        //Reset values since a new participant number has been entered
        pastTime = 0.0f;
        trialNumber = -1;
        timeOffset = 0;
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

    //Function that reads CSV file
    public void readCSV2(string csvPath2)
    {
        //Read in CSV file specified by csvPath input variable
        using(var reader = new StreamReader(csvPath2))
        {
            //Create a different string list for each CSV column
            List<string> listF = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                listF.Add(values[0]);

                data_sceneOrder = listF;
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
                addFinishedCommand(CommandsPath);
                //Loads the scene stored in the second to last element of the data_sceneCommmand list
                print("Loading " + lastElement + "...");
                SceneManager.LoadScene(lastElement, LoadSceneMode.Single);
            }

        //If most recent scene was the pop-up menu, open the scene commanded by the pop-up scene
            // if(data_sceneName[data_sceneName.Count - 1] == "moonScene_Eyetracking")
            // {
            //     print("Loading Popup Scene");
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

    //Function to return a string of the scene name with a number afterwards, either 1 2 or 3
    public string sceneCounter(string sName)
    {
        //Create new strings
        string sceneCounted;
        string sceneType;

        //Reset sNameCounter to 1
        sNameCounter = 1;

        //Call the readCSV function to update the data_trialNumber array
        readCSV(recentCSV);

        //Check which scene it is
        if(sName == "moonScene_Gaze")
        {
            sceneType = "Gaze";
        }
        else if(sName == "moonScene_Eyetracking")
        {
            sceneType = "Eyetracking";

        }
        else if(sName == "moonScene_Voice")
        {
            sceneType = "Voice";

        }
        else if(sName == "moonScene_Gesture")
        {
            sceneType = "Gesture";

        }
        else if(sName == "moonScene_PopUpWindow")
        {
            sceneType = "PopUpWindow";

        }
        else
        {
            sceneType = sName;
        }

        //Count how many times it has be listed in CSV file, if it exists
        if(data_sceneName[1] != "null")
        {
            //Loop through the data and see how many times sName occurs
            for(int k = 0; k < data_trialNumber.Count; k++)
            {
                //If the scene name in the CSV starts with the same name as the current scene, increment counter
                if(data_sceneName[k].StartsWith(sceneType))
                {
                    sNameCounter++;
                }
            }
        }

        //Append number onto scene type string
        sceneCounted = sceneType + " " + sNameCounter.ToString();
        return sceneCounted;
    }

    public void shuffleScenes()
    {
        //Initialize the sceneArray with 15 scene string names
        sceneArray = new string[]
        {
            "moonScene_Gaze",
            "moonScene_Gaze",
            "moonScene_Gaze",
            "moonScene_Eyetracking",
            "moonScene_Eyetracking",
            "moonScene_Eyetracking",
            "moonScene_Voice",
            "moonScene_Voice",
            "moonScene_Voice",
            "moonScene_Gesture",
            "moonScene_Gesture",
            "moonScene_Gesture",
            "moonScene_PopUpWindow",
            "moonScene_PopUpWindow",
            "moonScene_PopUpWindow"
        };

        for (int i = sceneArray.Length - 1; i > 0; i--)
        {
            int randomIndex = ran.Next(0, i + 1);
            
            string temp = sceneArray[i];
            sceneArray[i] = sceneArray[randomIndex];
            sceneArray[randomIndex] = temp;
        }

            // while(sceneArray[14] == sceneArray[13])
            // {   
            //     //Only shuffle everything from 13 and before
            //     for (int i = 0; i < 14; i++)
            //     {
            //         int randomIndex = ran.Next(0, i + 1);
                    
            //         string temp = sceneArray[i];
            //         sceneArray[i] = sceneArray[randomIndex];
            //         sceneArray[randomIndex] = temp;
            //     }
            // }
        
        //If one scene repeats itself, shuffle the remaining in descending order, down to the first two elements
        for (int j = sceneArray.Length - 1; j > 1; j--)
        {
            while(sceneArray[j] == sceneArray[j-1])
            {   
                //Only shuffle everything from j-1 and before
                for (int i = 0; i < j; i++)
                {
                    int randomIndex = ran.Next(0, i + 1);
                    
                    string temp = sceneArray[i];
                    sceneArray[i] = sceneArray[randomIndex];
                    sceneArray[randomIndex] = temp;
                }
            }
        }

        //Initialize differentIndex
        int differentIndex = sceneArray.Length - 1;

        //While the first two elements are the same, switch the 0 index with another index that is different
        while(sceneArray[0] == sceneArray[1])
        {   
            //Swap the first string with a the differentIndex string
            string temp2 = sceneArray[0];
            sceneArray[0] = sceneArray[differentIndex];
            sceneArray[differentIndex] = temp2;
            differentIndex--;
        }

        sceneFilePath = participantID + "_SceneOrder.csv";

        //Write this to a CSV called "Participant##_SceneOrder.csv"
        for (int i = 0; i < sceneArray.Length; i++)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@sceneFilePath, true))
                {
                    //Save data as a new line in CSV file
                    file.WriteLine(sceneArray[i] + ",");
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("This program did an oopsie :", ex);
            }
        }

        print("Scenes have been shuffled.");
    }

    private void SubmitParticipantNumber()
    {
        //Shuffle the sceneArray and save to a CSV
        shuffleScenes();
    }

    public void loadSceneCSV(string sFilePath)
    {
        
        if(data_sceneOrder[1] == "null")
        {
        readCSV2(@sFilePath);
        }

        string sceneToLoad = data_sceneOrder[sceneLoadIndex];
        print("The next scene to load is" + sceneToLoad);
        sceneLoadIndex++;
        
        //Load scene, or executable if sceneToLoad specifies popupwindow
        if(sceneToLoad == "moonScene_PopUpWindow" )
        {
            Process.Start(@"C:/Users/kdy7991/Desktop/Build_Files/PopUp_Window/VRHUD_Handtracking.exe");
            // Process.Start(@"C:\Users\nmchenry1\Desktop\Build_Files\PopUp_Window\VRHUD_Handtracking.exe");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}