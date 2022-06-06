using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField]
    float enemyHealth = 100f;

    [SerializeField]
    int enemyScore = 50;

    [Header("Enemy Projectile Settings")]

    [SerializeField]
    bool canShoot = false;

    [SerializeField]
    GameObject enemyProjectile;

    [SerializeField]
    float laserSpeed = 1f;

    [SerializeField]
    float minTimebwShots = 1f;

    [SerializeField]
    float maxTimebwShots = 3f;

    [Header("Enemy Effects")]

    [SerializeField]
    GameObject blast;

    [SerializeField]
    AudioClip deathAudio;

    [Range(0, 1)]
    [SerializeField]
    float audioVolume = 0.7f;

    float shotCounter;
    GameManager scoreManager;
    

    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimebwShots, maxTimebwShots);
        scoreManager = FindObjectOfType<GameManager>();
        
        
    }

    private void Update()
    {
        if (canShoot)
        {
            CoundDownAndShoot();
        }
    }

    private void CoundDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimebwShots, maxTimebwShots);
        }
    }

    private void Fire()
    {
        var newEnemyLaser = Instantiate<GameObject>(enemyProjectile, transform.position, Quaternion.identity);
        var shootLaser = newEnemyLaser.GetComponent<Rigidbody2D>();
        shootLaser.velocity = new Vector2(0f, -laserSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (other.name == "Player")
        {
            Destroy(gameObject);
            var blastVFX = Instantiate<GameObject>(blast, transform.position, Quaternion.identity);
            Destroy(blastVFX, 2f);
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, audioVolume);

        }
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.getDamage();
        damageDealer.Hit();
        if (enemyHealth <= 0)
        {
            scoreManager.CalculateScore(enemyScore);
            Destroy(gameObject);
            var blastVFX = Instantiate<GameObject>(blast, transform.position, Quaternion.identity);
            Destroy(blastVFX, 2f);
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, audioVolume);
        }
    }

}

 
