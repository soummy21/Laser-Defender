using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Controls")]

    [SerializeField]
    float playerSpeed = 8f;

    [SerializeField]
    float padding = 0.5f;

    [Header("Player")]

    [SerializeField]
    float playerHealth = 200f;

    [SerializeField]
    AudioClip playerHurtAudio;

    [Range(0,1)]
    [SerializeField]
    float hurtVolume = 0.5f;

    [SerializeField]
    AudioClip playerDeathAudio;

    [Range(0, 1)]
    [SerializeField]
    float deathVolume = 1f;

    [SerializeField]
    GameObject playerDeathVFX;

    [Header("Laser Settings")]

    [SerializeField]
    GameObject laser;

    [SerializeField]
    float projectileSpeed = 12f;

    [SerializeField]
    float laserFireTime = 0.1f;

    

   

    //cache variables
    Coroutine fireLaser;
    float xMin, xMax, yMin, yMax;
    Rigidbody2D laserBody;
    LevelLoader levelLoader;
    HealthBar healthBar;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        ClampVar();
        levelLoader = FindObjectOfType<LevelLoader>();
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(playerHealth);
        audioSource = GetComponent<AudioSource>();
    }

    

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
    }

    private void MovePlayer()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var xPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var yPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);
        transform.position = new Vector2(xPos, yPos);
    }

    IEnumerator ShootContinuosly()
    {
        while (true)
        {
            GameObject laserInstance = Instantiate<GameObject>(laser, transform.position, Quaternion.identity);
            laserBody = laserInstance.GetComponent<Rigidbody2D>();
            laserBody.AddForce(new Vector2(0f, projectileSpeed), ForceMode2D.Impulse);
            yield return new WaitForSeconds(laserFireTime);
        }
    }

    private void ClampVar()
    {
        Camera gameCamera;
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           fireLaser= StartCoroutine(ShootContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireLaser);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        PlayerDamage(damageDealer);

    }

    private void PlayerDamage(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.getDamage();
        healthBar.SetHealth(playerHealth);
        audioSource.PlayOneShot(playerHurtAudio, hurtVolume);
        damageDealer.Hit();
        if (playerHealth <= 0)
        {
            levelLoader.LoadEndScene();
            Destroy(gameObject);
            GenerateVFX();
            AudioSource.PlayClipAtPoint(playerDeathAudio, Camera.main.transform.position, deathVolume);
            

        }    
    }

    private void GenerateVFX()
    {
        var newVFX = Instantiate(playerDeathVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(newVFX, 6f);
    }
}


