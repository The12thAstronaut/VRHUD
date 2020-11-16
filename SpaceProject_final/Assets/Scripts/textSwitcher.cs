using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textSwitcher : MonoBehaviour
{
    public TextMeshPro[] textArray;
    private string[,] textPairArray = new string[6,2];

    void Start()
    {

        for (int i=0 ; i<=5; i++)
        {
            //Store all items in textArray as pairs (fruit name and fruit price)
            textPairArray[i,0] = textArray[2*i].text;
            textPairArray[i,1] = textArray[2*i+1].text;
            Debug.Log("FirstIndex " + 2*i + textPairArray[i,0]);
        }

        //Example of how to arrange text in reverse order
        // arrangeText(6,5,4,3,2,1,textPairArray);
    }

    void Update()
    {
        //Randomizes text order if the "A" key is pressed
        if(Input.GetKeyDown(KeyCode.A)){
            arrangeText(
                        Random.Range(1,7),
                        Random.Range(1,7),
                        Random.Range(1,7),
                        Random.Range(1,7),
                        Random.Range(1,7),
                        Random.Range(1,7),
                        textPairArray
                        );
        }
        //Orders scroll menu item #1 to be at the end if the "1" keyboard button is pressed
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            arrangeText(6,5,4,3,2,1, textPairArray);
        }

        //Orders scroll menu item #2 to be at the end if the "2" keyboard button is pressed
        if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            arrangeText(1,3,5,4,6,2, textPairArray);
        }

        //Orders scroll menu item #3 to be at the end if the "3" keyboard button is pressed
        if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            arrangeText(4,6,2,1,5,3, textPairArray);
        }

    //Moves text from one point in the array to another
    void copyText(int startIndex, int endIndex)
    {
        textArray[endIndex].text = textArray[startIndex].text;
    }

    //Arranges pairs of text in order specified
    void arrangeText(
                     int pair1Order,
                     int pair2Order,
                     int pair3Order,
                     int pair4Order,
                     int pair5Order,
                     int pair6Order,
                     string[,] PairArray
                    )
    {
        //Subtract 1 from each pairOrder int to match index from 0 to 5
            pair1Order = pair1Order - 1;
            pair2Order = pair2Order - 1;
            pair3Order = pair3Order - 1;
            pair4Order = pair4Order - 1;
            pair5Order = pair5Order - 1;
            pair6Order = pair6Order - 1;

        //Assign text values for each pair at correct spot
            textArray[2*pair1Order].text = PairArray[0,0];
            textArray[2*pair1Order+1].text = PairArray[0,1];

            textArray[2*pair2Order].text = PairArray[1,0];
            textArray[2*pair2Order+1].text = PairArray[1,1];

            textArray[2*pair3Order].text = PairArray[2,0];
            textArray[2*pair3Order+1].text = PairArray[2,1];

            textArray[2*pair4Order].text = PairArray[3,0];
            textArray[2*pair4Order+1].text = PairArray[3,1];

            textArray[2*pair5Order].text = PairArray[4,0];
            textArray[2*pair5Order+1].text = PairArray[4,1];

            textArray[2*pair6Order].text = PairArray[5,0];
            textArray[2*pair6Order+1].text = PairArray[5,1];
            // Debug.Log("PairArray1: " + PairArray[5,0].text);
            // Debug.Log("PairArray2: " + PairArray[5,1].text);
            // Debug.Log("textArray[2*pair6Order]1: " + textArray[2*pair6Order].text);
            // Debug.Log("textArray[2*pair6Order]2: " + textArray[2*pair6Order+1].text);
                for (int i=0 ; i<=5; i++)
                {
                    Debug.Log("Index " + 2*i + PairArray[i,0]);
                }

    }
}
