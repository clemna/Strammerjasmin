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
        UIManager.manager.ResetSceneSouls();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));

    }

    public void LoadCurrentLevel()
    {
        UIManager.manager.ResetScore();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void SkipLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        
    }
}
