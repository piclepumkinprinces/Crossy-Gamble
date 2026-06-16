using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

}
