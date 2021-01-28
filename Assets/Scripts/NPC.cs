using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public AudioSource scream;
    
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        scream = GetComponent<AudioSource>();
    }
    
    private Color GetRandomColorWithAlpha()
    {
        return new Color(
            Random.Range(0f,1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // Color randomlySelectedColor = GetRandomColorWithAlpha();
            // GetComponent<Renderer>().material.color = randomlySelectedColor;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            scream.Play();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
