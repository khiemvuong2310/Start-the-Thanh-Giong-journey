using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private Canvas shopCanvas; 
    [SerializeField] private GameObject shopUI; 

    private void Start()
    {
        //if (UIManager.Instance != null)
        //{
        //    UIManager.Instance.HidePlayerUI();
        //}

        shopCanvas.enabled = false;
        shopUI.SetActive(false);

        // Kiểm tra nếu UIFade chưa kích hoạt thì bật nó lên
        if (UIFade.Instance == null)
        {
            Debug.LogError("UIFade instance not found!");
        }
        else
        {
            StartCoroutine(ShowShopUI());
        }
    }


    private IEnumerator ShowShopUI()
    {
        yield return new WaitForSeconds(0.5f); 

        if (UIFade.Instance != null)
        {
            UIFade.Instance.FadeToClear();
        }

        yield return new WaitForSeconds(0.5f); 

        shopCanvas.enabled = true;
        shopUI.SetActive(true);
    }
}
