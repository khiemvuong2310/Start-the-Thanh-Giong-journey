using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; // Giá trị mặc định

    private void Start()
    {
        UpdateDamageFromWeapon();
    }

    private void OnEnable()
    {
        UpdateDamageFromWeapon();
    }

    private void UpdateDamageFromWeapon()
    {
        if (Sword.Instance != null)
        {
            WeaponInfo weaponInfo = Sword.Instance.GetWeaponInfo();
            if (weaponInfo != null)
            {
                damageAmount = weaponInfo.weaponDamage;
                Debug.Log("DamageSource updated damage to: " + damageAmount);
            }
        }
    }

    public void SetDamageAmount(int newDamage)
    {
        damageAmount = newDamage;
        Debug.Log("DamageSource damage set to: " + damageAmount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Debug.Log("Dealing damage: " + damageAmount);
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
