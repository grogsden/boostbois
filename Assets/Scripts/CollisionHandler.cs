using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{  
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) {return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You collided with Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Default Switch");
                StartCrashSequence();
                break;
        }
    }

  void StartSuccessSequence()
  {
    isTransitioning = true;

    audioSource.Stop();
    audioSource.PlayOneShot(success);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("NextLevel", levelLoadDelay);
  }

  void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        Debug.Log("Level Reloaded");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    
    

}