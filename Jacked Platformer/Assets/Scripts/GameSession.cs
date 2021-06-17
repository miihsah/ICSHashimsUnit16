using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class GameSession : MonoBehaviour
{
   
    [SerializeField] int playerLives = 5;//player starts with 5 lives 
    
    //This instance will always exist 
    private void Awake()
    {
        // find all game session objects
        int numGameSesssions = FindObjectsOfType<GameSession>().Length;
        //if more than one game session present, destroy the session trying to instantiate
        if (numGameSesssions > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }
    //This script is singleton
    void Start()
    {

    }
    // This method determines what happens when player dies in terms of the game session
    //Public so other classes can track deaths 
    public void ProcessPlayerDeath()
    {
        // If the player has more than 1 life, than process the death
        if (playerLives > 1)
        {
            StartCoroutine("DelayedDeath");
        }
        else {
            // If the player has no more lives, restart the game 
            ResetGameSession();
        }
    
    }
    //This Ienumerator delays death animation for 2 seconds before restarting level
    IEnumerator DelayedDeath()
    {
        playerLives--;//takes 1 live from player

        yield return new WaitForSeconds(2f);//Delay for 2 seconds

        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//get the current scene index 

        SceneManager.LoadScene(currentSceneIndex);

    }
    public void ResetGameSession()
    {
        // Load main menu
        SceneManager.LoadScene(0);
        Destroy(gameObject);//destroy this instance
    }
}
