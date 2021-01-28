using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SoundMng : MonoBehaviour
{
    public static AudioClip playerHitSound;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("PlayerScream");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "PlayerScream":
                audioSrc.PlayOneShot(playerHitSound);
                break;
        }
    }
}




    
