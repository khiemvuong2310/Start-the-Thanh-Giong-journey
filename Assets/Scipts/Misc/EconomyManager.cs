using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text ";

    // Thêm getter để lấy số tiền hiện tại
    public int GetCurrentGold()
    {
        return currentGold;
    }

    // Thêm phương thức để chi tiêu
    public bool SpendGold(int amount)
    {
        // Kiểm tra xem có đủ tiền không
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldText();
            return true;
        }
        return false;
    }

    // Phương thức cập nhật hiển thị số tiền
    private void UpdateGoldText()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }

    public void UpdateCurrentGold()
    {
        currentGold += 1;

        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }

    // Thêm phương thức để thêm số lượng vàng tùy chỉnh
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldText();
    }
}
