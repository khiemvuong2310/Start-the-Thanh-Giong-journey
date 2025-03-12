using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private float arrowRange = 15f;

    public void Attack()
    {
        Vector2 targetDirection = (PlayerController.Instance.transform.position - transform.position).normalized;

        GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        newArrow.transform.right = targetDirection;

        Projectile arrowProjectile = newArrow.GetComponent<Projectile>();
        if (arrowProjectile != null)
        {
            arrowProjectile.SetSpeed(arrowSpeed);
            arrowProjectile.UpdateProjectileRange(arrowRange);
        }
    }
}

