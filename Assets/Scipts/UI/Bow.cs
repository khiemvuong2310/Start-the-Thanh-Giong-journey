using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public static Bow Instance { get; private set; } // ✅ Singleton

    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        myAnimator = GetComponent<Animator>();
        weaponInfo = Instantiate(weaponInfo);
    }

    public void Attack()
    {
        SoundManager.Instance.PlaySound2D("BowAttack");
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        
        // Cập nhật cả tầm bắn và sát thương cho mũi tên
        Projectile projectile = newArrow.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.UpdateProjectileRange(weaponInfo.weaponRange);
            projectile.UpdateProjectileDamage(weaponInfo.weaponDamage);
            Debug.Log("Arrow fired with damage: " + weaponInfo.weaponDamage);
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    
    // Thêm phương thức nâng cấp sát thương tương tự như Sword
    public void UpgradeWeaponDamage(int amount)
    {
        if (weaponInfo != null)
        {
            weaponInfo.IncreaseWeaponDamage(amount);
            Debug.Log("Bow damage increased! New damage: " + weaponInfo.weaponDamage);
        }
        else
        {
            Debug.LogWarning("WeaponInfo is not assigned for Bow!");
        }
    }
}

