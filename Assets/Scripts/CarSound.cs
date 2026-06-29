using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound  : MonoBehaviour
{
    [SerializeField] float minPitch = 0.6f;
    [SerializeField] float maxPitch = 1.6f;
    [SerializeField] float maxSpeed = 100f;


    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip brakeSound;
    [SerializeField] AudioClip hitSound;

    private AudioSource engineSource;
    private AudioSource sfxSource;
    private CarController carController;

    private bool isBraking = false;
    
    void Start()
    {
        AudioSource[] source = GetComponents<AudioSource>();
        engineSource = source[0];
        sfxSource = source[1];
        PlayEngineSound();

        carController = GetComponent<CarController>();
    }
    void Update()
    {
        UpdateEngineSoundPitch();
        HandleBrakeSound();
    }
    private void PlayEngineSound()
    {
        engineSource.clip = engineSound;
        engineSource.volume = 0.3f;
        engineSource.loop = true;
        engineSource.Play();
    }
    private void UpdateEngineSoundPitch()
    {
        float speed = carController.CarSpeed();
        float normalizeSpeed = speed/maxSpeed;
        normalizeSpeed=Mathf.Clamp01(normalizeSpeed);
        engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizeSpeed);
    }
    private void HandleBrakeSound()
    {
        if(Input.GetKey(KeyCode.Space)&&carController.CarSpeed()>10f)
        {
            if(!isBraking)
            {
                isBraking = true;
                sfxSource.clip = brakeSound;
                sfxSource.loop = true;
                sfxSource.Play();
            }
        }
        else
        {
            if(isBraking)
            {
                isBraking = false;
                sfxSource.Stop();
            }
        }
    }
    public void PlayHitSound()
    {
        sfxSource.volume = 0.4f;
        sfxSource.PlayOneShot(hitSound);
    }
    public void StopAllCarSounds()
    {
        if(engineSource.isPlaying)
        engineSource.Stop();
        if(sfxSource.isPlaying)
        sfxSource.Stop();
    }
}
