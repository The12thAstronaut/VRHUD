using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class KeyWordRecognizerBehaviour : MonoBehaviour
{
	
	KeywordRecognizer keywordRecognizer;
	// keyword array
	public string[] Keywords_array;
    public GameObject Menu;
    public Interactable ScrollDownButton;
    public Interactable ScrollUpButton;


    void Start()
    {
		// instantiate keyword recognizer, pass keyword array in the constructor
		keywordRecognizer = new KeywordRecognizer(Keywords_array);
		keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
		// start keyword recognizer
		keywordRecognizer.Start ();
    }

void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log ("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: "  + args.phraseDuration);
		// write your own logic here for voice commands based off of keywords in the keyword_array

        if(args.text == "Scroll Down"){
            ScrollDownButton.TriggerOnClick();
        }
        if(args.text == "Scroll Up"){
			ScrollUpButton.TriggerOnClick();
        }
        if(args.text == "Hide Menu"){
			Menu.SetActive(false);

        }
        if(args.text == "Show Menu"){
            Menu.SetActive(true);
        }
	}

}
