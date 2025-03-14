using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    public static Sword Instance { get; private set; } // ✅ Singleton

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnim;

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

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
        
        // Cập nhật sát thương cho DamageSource khi khởi tạo
        UpdateDamageSourceDamage();
    }

    public void UpgradeWeaponDamage(int amount)
    {
        if (weaponInfo != null)
        {
            weaponInfo.IncreaseWeaponDamage(amount);
            Debug.Log("Weapon damage increased! New damage: " + weaponInfo.weaponDamage);
            
            UpdateDamageSourceDamage();
        }
        else
        {
            Debug.LogWarning("WeaponInfo is not assigned!");
        }
    }

    // Phương thức mới để cập nhật sát thương cho tất cả DamageSource
    private void UpdateDamageSourceDamage()
    {
        if (weaponCollider != null)
        {
            DamageSource damageSource = weaponCollider.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                damageSource.SetDamageAmount(weaponInfo.weaponDamage);
            }
            else
            {
                Debug.LogWarning("DamageSource component not found on weaponCollider!");
            }
        }
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        SoundManager.Instance.PlaySound2D("SwordAttack");
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }


    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
