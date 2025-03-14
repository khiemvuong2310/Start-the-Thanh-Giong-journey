﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Không được bỏ nha liên quan đến arrow attack á
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    [SerializeField] private int projectileDamage = 1;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void UpdateProjectileDamage(int damage)
    {
        this.projectileDamage = damage;
        Debug.Log("Projectile damage set to: " + projectileDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            //if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            //{
            //    player?.TakeDamage(1, transform);
            //    Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            //    Destroy(gameObject);
            //}
            if (enemyHealth && !isEnemyProjectile)
            {
                Debug.Log($"Enemy hit! Dealing damage: {projectileDamage}");
                enemyHealth.TakeDamage(projectileDamage);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (player && isEnemyProjectile)
            {
                player.TakeDamage(projectileDamage, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            else if (!other.isTrigger && indestructible)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}

