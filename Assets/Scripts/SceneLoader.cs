using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
}