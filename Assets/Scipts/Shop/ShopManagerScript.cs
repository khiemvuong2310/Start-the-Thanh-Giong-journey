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
    public float coins;
    void Start()
    {
        UpdateCoinUI();

        //CoinTxt.text = coins.ToString();
        // ID sản phẩm
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

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
            // Nếu mua item có ID là 1, gọi phương thức HealPlayer
            if (itemID == 1)
            {
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("Người chơi đã được hồi máu!");
            }
            if (itemID == 2)
            {
                Stamina.Instance.RefreshStamina();
                Debug.Log("Người chơi đã được hồi stamina!");
            }
            if (itemID == 3)
            {
                Sword.Instance.UpgradeWeaponDamage(1);
                Debug.Log("Người chơi đã được up power!");
            }
        }
        else
        {
            Debug.LogWarning("Không đủ tiền để mua item ID " + itemID);
        }

        //if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        //{
        //    coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
        //    shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
        //    CoinTxt.text = coins.ToString();
        //    buttonInfo.QuantityTxt.text = shopItems[3, buttonInfo.ItemID].ToString();
        //}
    }
}
