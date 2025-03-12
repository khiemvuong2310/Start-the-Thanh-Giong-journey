using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneManager : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "Scene1"; // Tên scene chính để quay lại
    
    private void Awake()
    {
        // Kiểm tra xem EconomyManager đã tồn tại chưa
        if (EconomyManager.Instance == null)
        {
            Debug.LogError("EconomyManager không tồn tại! Cần đảm bảo nó được tạo và không bị hủy khi chuyển scene.");
        }
    }
    
    private void Start()
    {
        // Đảm bảo ShopManagerScript được cập nhật với số tiền hiện tại
        ShopManagerScript shopManager = FindObjectOfType<ShopManagerScript>();
        if (shopManager != null)
        {
            // ShopManagerScript sẽ tự động cập nhật trong Start()
        }
        else
        {
            Debug.LogWarning("Không tìm thấy ShopManagerScript trong scene!");
        }
    }
    
    // Phương thức để quay lại scene chính
    public void ReturnToMainScene()
    {
        StartCoroutine(ReturnToMainSceneRoutine());
    }
    
    private IEnumerator ReturnToMainSceneRoutine()
    {
        // Hiệu ứng Fade nếu có
        if (UIFade.Instance != null)
        {
            UIFade.Instance.FadeToBlack();
            yield return new WaitForSeconds(1f); // Đợi hiệu ứng fade hoàn thành
        }
        
        // Chuyển về scene chính
        SceneManager.LoadScene(mainSceneName);
    }
    
    // Phương thức này có thể được gọi từ nút "Return" trong UI Shop
    public void OnReturnButtonClicked()
    {
        ReturnToMainScene();
    }
} 