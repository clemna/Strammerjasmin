using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public GameObject Continue;
    public LevelLoader Loader;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitContinue());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Loader.LoadNextLevel();
        }
    }

    IEnumerator WaitContinue()
    {
        yield return new WaitForSeconds(5);

        Continue.SetActive(true);

    }
}
