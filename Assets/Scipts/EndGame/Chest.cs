using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX; 
    [SerializeField] private GameObject keyPrefab;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            DropKey();

            if (destroyVFX != null)
            {
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
            }
            
            // Hủy rương
            Destroy(gameObject);
        }
    }


    private void DropKey()
    {
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
    }
}
