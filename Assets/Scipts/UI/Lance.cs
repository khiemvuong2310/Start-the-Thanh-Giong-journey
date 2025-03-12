using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private float lanceAttackCD = 0.7f; // Thời gian hồi chiêu

    private Transform weaponCollider;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
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
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    //private void MouseFollowWithOffset()
    //{
    //    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mouseWorldPos.z = 0f; // Đảm bảo vũ khí luôn trên mặt phẳng 2D

    //    Vector3 direction = (mouseWorldPos - PlayerController.Instance.transform.position).normalized;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    // Kiểm tra hướng của nhân vật
    //    bool facingLeft = PlayerController.Instance.FacingLeft;

    //    if (facingLeft)
    //    {
    //        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, -angle);
    //        weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
    //    }
    //    else
    //    {
    //        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    //        weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, mousePos.x - playerScreenPoint.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, -angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    //private void MouseFollowWithOffset()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

    //    float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, mousePos.x - playerScreenPoint.x) * Mathf.Rad2Deg;

    //    if (mousePos.x < playerScreenPoint.x)
    //    {
    //        transform.rotation = Quaternion.Euler(0, 180, -angle);
    //        weaponCollider.rotation = Quaternion.Euler(0, 180, -angle); // Thêm dòng này
    //    }
    //    else
    //    {
    //        transform.rotation = Quaternion.Euler(0, 0, angle);
    //        weaponCollider.rotation = Quaternion.Euler(0, 0, angle); // Thêm dòng này
    //    }
    //}
}

