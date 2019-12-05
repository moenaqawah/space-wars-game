using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 500;
    [SerializeField] float laserSpeed = 5f;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] int killingScore=10;
    // the higher the value the lower the sound
   [Range(0f,1f)] [SerializeField] float deathVolume=0.5f;
    
    [Header("Enemy Laser")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] AudioClip enemyLaserSFX;
    // the higher the value the lower the sound
    [Range(0f, 1f)] [SerializeField] private float Laservolume = 0.7f;


    float shotCounter;
    GameSession gameSession;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        countDownAndShoot();
    }

    private void countDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void fire()
    {
        var newLaser = Instantiate(enemyLaser, new Vector3(transform.position.x,transform.position.y,transform.position.z+1) , Quaternion.identity);
        var rb=newLaser.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSFX, Camera.main.transform.position,Laservolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        var playerDamageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!playerDamageDealer)
        {
            return;
        }

        processHit(playerDamageDealer);
        
    }

    private void processHit(DamageDealer playerDamageDealer)
    {
        playerDamageDealer.Hit();
        health -= playerDamageDealer.getDamage();
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {

        gameSession.addToScore(killingScore);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position,deathVolume);
        var exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(exp, 1);
    }
}
