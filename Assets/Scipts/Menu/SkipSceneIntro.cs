using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkipSceneIntro : MonoBehaviour
{
    public void Skip()
    {
        SceneManager.LoadScene("Scene1");
    }

}
