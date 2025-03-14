using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4]; 
    public TMP_Text CoinTxt;
    // Biến coins không còn cần thiết vì chúng ta sử dụng EconomyManager
    // public float coins;
    
    void Start()
    {
        UpdateCoinUI();

        // ID sản phẩm
        shopItems[1, 1] = 1; // Hồi máu
        shopItems[1, 2] = 2; // Hồi stamina
        shopItems[1, 3] = 3; // Nâng cấp sát thương

        // Giá sản phẩm
        shopItems[2, 1] = 3;
        shopItems[2, 2] = 1;
        shopItems[2, 3] = 2;

        // Số lượng sản phẩm đã mua
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
    }
    
    // ✅ Phương thức cập nhật số tiền trên UI
    private void UpdateCoinUI()
    {
        if (CoinTxt != null)
        {
            CoinTxt.text = EconomyManager.Instance.GetCurrentGold().ToString("D3");
        }
        else
        {
            Debug.LogError("CoinTxt chưa được gán trong ShopManager!");
        }
    }
    
    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (ButtonRef == null)
        {
            Debug.LogWarning("Không tìm thấy Button được nhấn.");
            return;
        }

        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();
        if (buttonInfo == null)
        {
            Debug.LogWarning("Không tìm thấy ButtonInfo trên Button: " + ButtonRef.name);
            return;
        }

        int itemID = buttonInfo.ItemID;
        Debug.Log("Nhấn vào nút có ItemID: " + itemID);

        if (itemID < 1 || itemID > 3)
        {
            Debug.LogWarning("Item ID không hợp lệ: " + itemID);
            return;
        }

        int price = shopItems[2, itemID];

        // Sử dụng EconomyManager để kiểm tra và trừ tiền
        if (EconomyManager.Instance.SpendGold(price))
        {
            shopItems[3, itemID]++;

            // Cập nhật UI
            buttonInfo.QuantityTxt.text = shopItems[3, itemID].ToString();
            buttonInfo.UpdateUI();
            UpdateCoinUI();

            Debug.Log("Đã mua item ID " + itemID + ". Còn lại: " + EconomyManager.Instance.GetCurrentGold() + " coins.");
            
            // Xử lý hiệu ứng của item dựa trên ID
            switch (itemID)
            {
                case 1: // Hồi máu
                    PlayerHealth.Instance.HealPlayer();
                    Debug.Log("Người chơi đã được hồi máu!");
                    // Phát âm thanh hồi máu nếu có
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlaySound2D("Heal");
                    }
                    break;
                    
                case 2: // Hồi stamina
                    Stamina.Instance.RefreshStamina();
                    Debug.Log("Người chơi đã được hồi stamina!");
                    // Phát âm thanh hồi stamina nếu có
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlaySound2D("Stamina");
                    }
                    break;
                    
                case 3: // Nâng cấp sát thương
                    // Nâng cấp sát thương cho vũ khí hiện tại
                    UpgradeCurrentWeapon();
                    Debug.Log("Người chơi đã được up power!");
                    // Phát âm thanh nâng cấp nếu có
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlaySound2D("PowerUp");
                    }
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Không đủ tiền để mua item ID " + itemID);
            // Phát âm thanh không đủ tiền nếu có
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound2D("Error");
            }
        }
    }

    /// <summary>
    /// Phương thức nâng cấp sát thương cho vũ khí hiện tại của người chơi.
    /// Hỗ trợ các loại vũ khí: Sword, Bow, Staff, Lance.
    /// </summary>
    private void UpgradeCurrentWeapon()
    {
        // Kiểm tra vũ khí hiện tại và nâng cấp sát thương
        if (ActiveWeapon.Instance == null)
        {
            Debug.LogWarning("ActiveWeapon.Instance không tồn tại!");
            return;
        }
        
        MonoBehaviour currentWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        
        if (currentWeapon == null)
        {
            Debug.LogWarning("Người chơi không có vũ khí nào được trang bị!");
            return;
        }
        
        // Nâng cấp sát thương dựa trên loại vũ khí
        if (currentWeapon is Sword)
        {
            // Nâng cấp sát thương cho Sword
            // Sword.Instance.UpgradeWeaponDamage sẽ tự động cập nhật DamageSource
            Sword.Instance.UpgradeWeaponDamage(1);
            Debug.Log("Đã nâng cấp sát thương cho Sword!");
        }
        else if (currentWeapon is Bow)
        {
            // Nâng cấp sát thương cho Bow
            // Bow.Instance.UpgradeWeaponDamage sẽ tự động cập nhật sát thương cho Projectile khi bắn
            Bow.Instance.UpgradeWeaponDamage(1);
            Debug.Log("Đã nâng cấp sát thương cho Bow!");
        }
        else if (currentWeapon is Staff)
        {
            // Nếu có Staff, thêm code tương tự ở đây
            Debug.Log("Staff chưa được hỗ trợ nâng cấp sát thương!");
        }
        else if (currentWeapon is Lance)
        {
            // Nếu có Lance, thêm code tương tự ở đây
            Debug.Log("Lance chưa được hỗ trợ nâng cấp sát thương!");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy vũ khí để nâng cấp!");
        }
    }
}
