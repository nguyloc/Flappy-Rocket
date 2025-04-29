using UnityEngine;
using UnityEngine.SceneManagement;
    
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLoadLevel = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    AudioSource _audioSource;
    
    bool isControllable = true;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    // This script handles collision events for the player ship.
    // It checks for collisions with different objects and handles them accordingly.
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;
            
        // Check the tag of the object we collided with.
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected.");
                break;
            
            case "Fuel":
                Debug.Log("Fuel collision detected.");
                break;

            case "Finish":
                StartSuccessSequence();
                break;
            
            default:
                StartCrashSequence();
                break;
        }
    }
    
    // This method starts the success sequence, which loads the next level after a delay.
    void StartSuccessSequence()
    {
        isControllable = false;                         // Disable player control.
        _audioSource.Stop();                            // Stop any currently playing audio.
        _audioSource.PlayOneShot(success);              // Play success sound effect.
        GetComponent<Movement>().enabled = false;       // Disable movement script to stop player control.
        Invoke(nameof(LoadNextLevel), delayLoadLevel);  //delay before loading the next level.
    }

    // This method starts the crash sequence, which reloads the level after a delay.
    void StartCrashSequence()
    {
        isControllable = false;                         // Disable player control.
        _audioSource.Stop();                            // Stop any currently playing audio.
        _audioSource.PlayOneShot(crash);                // Play crash sound effect.
        GetComponent<Movement>().enabled = false;       // Disable movement script to stop player control.
        Invoke(nameof(ReloadLevel), delayLoadLevel);    // delay before reloading the level.
    }

    // This method reloads the current level.
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    // This method loads the next level in the build settings.
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings) nextScene = 0;
            
        SceneManager.LoadScene(nextScene);
    }
}
