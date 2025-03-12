using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject playerUI; // Thay vì tắt cả UICanvas, chỉ tắt phần Player UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ UI khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HidePlayerUI()
    {
        if (playerUI != null)
        {
            playerUI.SetActive(false); // Chỉ ẩn UI Player, không tắt cả UICanvas
        }
    }

    public void ShowPlayerUI()
    {
        if (playerUI != null)
        {
            playerUI.SetActive(true);
        }
    }
}


