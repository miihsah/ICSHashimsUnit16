using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Public so you can call this method when button is pressed 
    public void StartFirstLevel() {
        

        // Load scene 1
        SceneManager.LoadScene(1);

        
    }
    public void loadMainMenu()
    {


        // Load Main Menu and reset the lives
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGameSession();

    }


}
