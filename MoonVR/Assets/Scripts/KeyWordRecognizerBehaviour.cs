using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
 
public class KeyWordRecognizerBehaviour : MonoBehaviour {
	
	KeywordRecognizer keywordRecognizer;
	// keyword array
	public string[] Keywords_array;
    public GameObject Panel;
    public GameObject ColorSphereOne;
	public GameObject ColorSphereTwo;
	public GameObject ColorSphereThree;
    public GameObject SelectedSphere;
    public GameObject Task1;
    public GameObject Task2;

    public Material redMaterial;
    public Material blueMaterial;

	// Use this for initialization
	void Start () {
		// Change size of array for your requirement
		// Keywords_array = new string[2];
		// Keywords_array [0] = "hello";
		// Keywords_array [1] = "how are you";
 
		// instantiate keyword recognizer, pass keyword array in the constructor
		keywordRecognizer = new KeywordRecognizer(Keywords_array);
		keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
		// start keyword recognizer
		keywordRecognizer.Start ();

        //Set to Sphere One by default
        SelectedSphere = ColorSphereOne;

	}
 
	void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log ("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: "  + args.phraseDuration);
		// write your own logic here for voice commands based off of keywords in the keyword_array

        if(args.text == "Red"){
            SelectedSphere.GetComponent<Renderer>().material = redMaterial;
        }
        if(args.text == "Blue"){
            SelectedSphere.GetComponent<Renderer>().material = blueMaterial;
        }
        if(args.text == "Hide"){
            SelectedSphere.SetActive(false);
        }
        if(args.text == "Appear"){
            SelectedSphere.SetActive(true);
        }
        if(args.text == "Sphere One"){
            SelectedSphere = ColorSphereOne;
        }
        if(args.text == "Sphere Two"){
            SelectedSphere = ColorSphereTwo;
        }
        if(args.text == "Sphere Three"){
            SelectedSphere = ColorSphereThree;
        }
	}
}