using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SkipLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (UIManager.manager != null)
        {
            UIManager.manager.ResetSceneSouls();
        }
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));

    }

    public void LoadCurrentLevel()
    {
        if (UIManager.manager != null)
        {
            UIManager.manager.ResetScore();
        }
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadFirstLevel()
    {
        if (UIManager.manager != null)
        {
            UIManager.manager.ResetUI();
        }
        StartCoroutine(LoadLevel(1));
    }

    public void SkipLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) <= 16)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            LoadFirstLevel();
        }
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        
    }
}
