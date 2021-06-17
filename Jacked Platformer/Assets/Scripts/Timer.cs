using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 60;
    public bool takingAway = false;


    void Start()
    {
        textDisplay.GetComponent<Text>().text = "" + secondsLeft;// Display original time

    }

    void Update()
    {
        // if time is not counting down start countdown
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(Countdown());
            

        }
        // If timer runs out restart game
        else if (secondsLeft == 0)
        {


            FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }


        IEnumerator Countdown()
        {

            // Program starts taking away time 
            takingAway = true;
            yield return new WaitForSeconds(1); // wait 1 second 
            secondsLeft -= 1;//Take one second 
            textDisplay.GetComponent<Text>().text = "" + secondsLeft;// display new time
            takingAway = false;//Program reverts back to not taking away time 
           
        }
    }
}
