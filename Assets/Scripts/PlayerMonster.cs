using System;
using System.Collections;
using System.Collections.Generic;
using Antlr.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerMonster : MonoBehaviour
{
    public int minHealth = 0;
    public int maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    public float damage = 0;
    public float gain = 0;

    public AudioSource winMusic;
    public AudioSource munch;
    private AudioSource mainMusic;

    public GameObject winTextObject;
    public GameObject music;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        objects = FindObjectsOfType(typeof(AudioSource));
        currentHealth = minHealth;
        healthBar.SetMaxHealth(maxHealth);
        munch = GetComponent<AudioSource>();
        mainMusic = music.GetComponent<AudioSource>();
        winTextObject.SetActive(false);
    }

    private AudioSource[] allAudioSources;
 
    void StopAllAudio() {
        allAudioSources = objects as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
    }
    
    bool MyFunctionCalled = false;
    private Object[] objects;

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= 100)
        {
            if (MyFunctionCalled == false)
            {
                //StopAllAudio();
                mainMusic.Stop();
                winMusic.Play();
                winTextObject.SetActive(true);
                MyFunctionCalled = true;
            }
            
        }
    }

    void GainHealth(float add)
    {
        currentHealth += add;
        
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BirdieNPCZone"))
        {
            Debug.Log("Collision Detected");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BirdieNPCZone"))
        {
            munch.Play();
            other.GetComponentInParent<NPC>().TakeDamage(damage);
            GainHealth(gain);
        } else if (other.gameObject.CompareTag("BigRedNPCZoneIdle"))
        {
            munch.Play();
            other.GetComponentInParent<NPC>().TakeDamage(damage);
            GainHealth(gain);
        } else if (other.gameObject.CompareTag("BigRedNPCZoneRunning"))
        {
            munch.Play();
            other.GetComponentInParent<NPC>().TakeDamage(damage);
            GainHealth(gain);
        } else if (other.gameObject.CompareTag("CubieNPCZoneIdle"))
        {
            munch.Play();
            other.GetComponentInParent<NPC>().TakeDamage(damage);
            GainHealth(gain);
        } else if (other.gameObject.CompareTag("CubieNPCZoneRunning"))
        {
            munch.Play();
            other.GetComponentInParent<NPC>().TakeDamage(damage);
            GainHealth(gain);
        }
    }
}
