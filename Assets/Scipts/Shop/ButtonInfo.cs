using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TMP_Text PriceTxt;
    public TMP_Text QuantityTxt;

    public ShopManagerScript ShopManager;

    void Start()
    {
        if (ShopManager == null)
        {
            Debug.LogError("ShopManager chưa được gán trong ButtonInfo của Item ID " + ItemID);
            return;
        }

        UpdateUI(); // ✅ Cập nhật UI ngay khi ButtonInfo khởi tạo
    }


    public void UpdateUI()
    {
        if (ShopManager != null)
        {
            PriceTxt.text = "Price: " + ShopManager.shopItems[2, ItemID].ToString();
            QuantityTxt.text = ShopManager.shopItems[3, ItemID].ToString();
        }
    }

}
