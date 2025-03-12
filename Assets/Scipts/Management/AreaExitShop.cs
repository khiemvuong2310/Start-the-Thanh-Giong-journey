using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExitShop : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private GameObject player; // Player được gán từ Inspector
    [SerializeField] private Canvas playerCanvas; // Canvas trong Player (Screen1)

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Gán tên transition
            if (SceneManagement.Instance != null)
            {
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            }

            // Hiệu ứng Fade
            if (UIFade.Instance != null)
            {
                UIFade.Instance.FadeToBlack();
            }

            // Tắt Canvas của Player
            HidePlayerUI();

            // Chuyển Scene
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private void HidePlayerUI()
    {
        if (playerCanvas != null)
        {
            playerCanvas.enabled = false; // Tắt UI khi vào Shop
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
