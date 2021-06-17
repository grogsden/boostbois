using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 500f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem mainBoosterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
    {
      startThrusting();
    }
    else
    {
      stopThurstingEffects();
    }
  }

  void startThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainBoosterParticles.isPlaying)
    {
      mainBoosterParticles.Play();
    }
  }

   private void stopThurstingEffects()
  {
    audioSource.Stop();
    mainBoosterParticles.Stop();
  }

  void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
    {
      thrustRight();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      thurstLeft();
    }
    else
    {
      stopRotationEffects();
    }
  }

private void thrustRight()
  {
    ApplyRotation(rotationThrust);
    if (!rightBoosterParticles.isPlaying)
    {
      rightBoosterParticles.Play();
    }
  }
  private void thurstLeft()
  {
    ApplyRotation(-rotationThrust);
    if (!leftBoosterParticles.isPlaying)
    {
      leftBoosterParticles.Play();
    }
  }

  private void stopRotationEffects()
  {
    rightBoosterParticles.Stop();
    leftBoosterParticles.Stop();
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; //freezing roation so we can manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; //unfreezing rotation so physics system can take over
  }
}
