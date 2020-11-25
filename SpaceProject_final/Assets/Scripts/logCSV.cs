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

    public List<string> dataColumn1;
    public List<string> dataColumn2;
    public List<string> dataColumn3;
    public List<string> dataColumn4;
    public List<string> dataColumn5;

    public string[] csvFiles;
    public string recentCSV;


    void Awake()
    {   
        //Makes sure that all the data is together
        DontDestroyOnLoad(this.gameObject);

        //Read most recent CSV file data
            //Search for CSV files within project directory
            csvFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.csv");

            //Find most recent CSV file and save the string to the recentCSV variable
            var files = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory()).GetFiles("*.csv");
            DateTime lastModified = DateTime.MinValue;
            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastModified)
                {
                    lastModified = file.LastWriteTime;
                    recentCSV = file.Name;
                }
            }

            //Call the readCSV function and use the most recent CSV as an input
            readCSV(recentCSV);

        //Read CSV file data, hardcoded option
            // string CSVPath = @"C:\Users\nmchenry1\Documents\GitHub\VRHUD\SpaceProject_final\12_VRHUD_Task_Time.csv";
            // readCSV(CSVPath);
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

                dataColumn1 = listA;
                dataColumn2 = listB;
                dataColumn3 = listC;
                dataColumn4 = listD;
                dataColumn5 = listE;
            }
        }

        //If most recent scene was the pop-up menu, open the scene commanded by the pop-up scene
        //Else do nothing
    }
}