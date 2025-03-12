using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject UIPost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(EndGameCoroutine());
        }
    }

    private IEnumerator EndGameCoroutine()
    {
        CanvasGroup canvas = GameObject.FindGameObjectWithTag("VictoryEvent").GetComponent<CanvasGroup>();
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;

        Debug.Log("Trò chơi kết thúc! Chuyển cảnh sau 3 giây...");
        yield return new WaitForSeconds(3f); 

        SceneManager.LoadScene("VictoryScene"); 
    }
}
