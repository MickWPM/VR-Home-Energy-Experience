using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
