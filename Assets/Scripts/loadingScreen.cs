using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class loadingScreen : MonoBehaviour
{
    public TextMeshProUGUI percentText;
    public Image bar;
    float percentage;
    public GameObject finishedObject;

    private void Start()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        operation.allowSceneActivation = true;
        Debug.Log("Loading Scene");
        while (!operation.isDone)
        {
            float prog = operation.progress;
            percentage = prog * 100;
            percentText.text = percentage.ToString() + "%";
            bar.fillAmount = prog;
            yield return null;
        }
    }
}
