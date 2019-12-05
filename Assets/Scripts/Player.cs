using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float playerSpeed=5f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip playerDeathSFX;
    [Range(0,1)] [SerializeField] float playerDeathVolume = 1f;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float fireSpeed = 10f;
    [SerializeField] float timeBetweenLaser=3f;

    
    float xMin, xMax,yMin,yMax;
    Coroutine firingCorountine;
    




    // Start is called before the first frame update
    void Start()
    {
        setUpMoveBoundries();
    }

   

    // Update is called once per frame
    void Update()
    {
        move();
        fire();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyDamageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!enemyDamageDealer)
        {
            return;
        }
        processHit(enemyDamageDealer);
    }

    private void processHit(DamageDealer enemyDamageDealer)
    {
        enemyDamageDealer.Hit();
        health -= enemyDamageDealer.getDamage();
        if (health <= 0)
        {
            die();
        }
    }

    public int getHealth()
    {
        return health;
    }

    private void die()
    {
        FindObjectOfType<Level>().loadGameOverScene();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position,playerDeathVolume);
    }

    IEnumerator rapidFire()
    {
        while (true)
        {
            var laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fireSpeed);
            yield return new WaitForSeconds(timeBetweenLaser);
        }  
    }

    private void fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           firingCorountine=StartCoroutine(rapidFire());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCorountine);
        }

    }

    private void move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var newXPos = transform.position.x + deltaX * Time.deltaTime * playerSpeed;
        
        var deltaY = Input.GetAxis("Vertical");
        var newYPos = transform.position.y + deltaY * Time.deltaTime * playerSpeed;

        transform.position = new Vector2(Mathf.Clamp(newXPos,xMin+padding,xMax-padding) , Mathf.Clamp(newYPos, yMin + padding, yMax - padding));

    }

    private void setUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
