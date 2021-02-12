using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoControl : MonoBehaviour
{
    public LevelLoader Loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (this.gameObject.tag == "Intro")
            {
                Loader.LoadNextLevel();
            }
            else if (this.gameObject.tag == "End")
            {
                Loader.LoadFirstLevel();
            }
            else if (this.gameObject.tag == "Credits")
            {
                Loader.LoadFirstLevel();
            }
            
        }

        if (this.gameObject.tag == "Logos")
        {
            StartCoroutine(WaitLogos());
        }
        if (this.gameObject.tag == "End")
        {
            StartCoroutine(WaitEnd());
        }
    }


    IEnumerator WaitLogos()
    {

        yield return new WaitForSeconds(14);

        Loader.LoadNextLevel();

        //SceneManager.LoadScene(levelIndex);

    }

    IEnumerator WaitEnd()
    {

        yield return new WaitForSeconds(77);

        Loader.LoadFirstLevel();

        //SceneManager.LoadScene(levelIndex);

    }

    IEnumerator WaitCredits()
    {
        yield return new WaitForSeconds(49);

        Loader.LoadFirstLevel();

    }
}
