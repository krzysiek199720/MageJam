using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject tutorialPanel;

    public void StartGame()
    {
        StartCoroutine("LoadGame");
    }

    IEnumerator LoadGame()
    {
        CanvasGroup canvasGroup = tutorialPanel.GetComponent<CanvasGroup>();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    public void SkipScene()
    {
        SceneManager.LoadScene(1); 
    }

}
