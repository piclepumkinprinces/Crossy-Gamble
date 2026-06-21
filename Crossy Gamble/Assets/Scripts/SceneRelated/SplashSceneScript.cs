using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashSceneScript : MonoBehaviour
{
    public TMP_Text myText;

    private bool toggle = false;

    void Start()
    {
        StartCoroutine(SwitchTextRoutine());
        StartCoroutine(LoadMainMenuAfterDelay());
    }

    IEnumerator SwitchTextRoutine()
    {
        while (true)
        {
            if (toggle)
            {
                myText.text = "Loading Assets...";
            }
            else
            {
                myText.text = "Loading Assets..";
            }

            toggle = !toggle;

            yield return new WaitForSeconds(0.8f);
        }
    }
    IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainMenu");
    }
}