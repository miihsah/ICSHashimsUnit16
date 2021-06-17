using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // wait 2 seconds 
    [SerializeField] float LevelLoadDelay = 2f;
    //[SerializeField] float SlowMotion = 0.2f;

    // On collision with exit start coroutine 
    void OnTriggerEnter2D(Collider2D other)
    {
        // Coroutine makes level not load instantly 
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel() {
        
        // Yield with a delay
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        
        // Load the next scene in the build index 
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

}
