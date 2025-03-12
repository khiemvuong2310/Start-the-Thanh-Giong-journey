using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Slider progressBar;
    public GameObject transitionsContainer;

    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        Debug.Log("Starting LoadSceneAsync...");

        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);
        if (transition == null)
        {
            Debug.LogError("Transition not found!");
            yield break;
        }

        Debug.Log("Transition found, playing animation...");

        yield return transition.AnimateTransitionIn();

        Debug.Log("Transition animation completed, loading scene...");

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        progressBar.gameObject.SetActive(true);

        do
        {
            progressBar.value = scene.progress;
            Debug.Log($"Loading progress: {scene.progress}");
            yield return null;
        } while (scene.progress < 0.9f);

        Debug.Log("Scene almost loaded, waiting before activation...");

        yield return new WaitForSeconds(1f);

        scene.allowSceneActivation = true;
        progressBar.gameObject.SetActive(false);

        Debug.Log("Scene activated, playing transition out...");

        yield return transition.AnimateTransitionOut();
    }

}