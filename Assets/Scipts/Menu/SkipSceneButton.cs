using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneButton : MonoBehaviour
{
    [SerializeField] private Canvas playerCanvas; // Gán Canvas của Player từ Inspector

    public void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerCanvas = player.GetComponentInChildren<Canvas>();

            if (playerCanvas == null)
            {
                Debug.LogError("Không tìm thấy Canvas trong object con của Player!");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy GameObject có tag 'Player'!");
        }
    }
    public void SkipScene()
    {
        // Bật lại Canvas của Player khi thoát Shop
        if (playerCanvas != null)
        {
            playerCanvas.enabled = true;
        }

        // Chuyển Scene
        SceneManager.LoadScene("Scene2");
    }
}
