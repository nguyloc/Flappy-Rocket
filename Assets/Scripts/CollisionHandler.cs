using System;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected.");
                break;
            
            case "Finish":
                LoadNextLevel();
                break;
            
            case "Fuel":
                Debug.Log("Fuel collision detected.");
                break;
            
            default:
                ReloadLevel();
                break;
        }

        void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        void LoadNextLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;

            if (nextScene == SceneManager.sceneCountInBuildSettings) nextScene = 0;
            
            SceneManager.LoadScene(nextScene);
        }
    }
}
