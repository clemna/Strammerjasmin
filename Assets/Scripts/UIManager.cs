using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager manager;

    [SerializeField]
    private GameObject pauseUI = null;
    static UIManager instance;

    static public int souls = 0;
    static private int sceneSouls = 0;
    private Health playerHealth;

    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private GameObject iconPrefab = null;
    [SerializeField]
    private Transform iconHolder = null;
    [SerializeField]
    private Vector3 iconOffset = Vector3.zero;
    private List<GameObject> iconInstances;

    private float lastPauseUIToggle;



    private void Awake()
    {
        //manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
        if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
                manager = this;
                GameObject.DontDestroyOnLoad(this.gameObject);
            }
            //instance = this;
            //GameObject.DontDestroyOnLoad(this.gameObject);
        
        //playerHealth = PlayerMovement.player.GetComponent<Health>();
        //iconInstances = new List<GameObject>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    /*void Update()
    {
        int health = playerHealth.GetCurrent();
        while (health < iconInstances.Count)
        {
            Vector3 position = iconHolder.position + iconOffset * iconInstances.Count;
            GameObject iconInstance = GameObject.Instantiate(iconPrefab, position, Quaternion.identity, iconHolder);
            iconInstances.Add(iconInstance);
        }
        // if more icons are there as should be
        while (health < iconInstances.Count)
        {
            GameObject iconInstance = iconInstances[iconInstances.Count - 1];
            iconInstances.RemoveAt(iconInstances.Count - 1);
            Destroy(iconInstance);
        }

        if (Input.GetButton("Cancel"))
        {
            if (Time.time > lastPauseUIToggle + 0.5f)
            {
                TogglePauseUI();
            }
        }
    }*/


    private void LateUpdate()
    {
        scoreText.text = souls.ToString();
    }

    public void TogglePauseUI()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            lastPauseUIToggle = Time.time;
        }
    }


    public void AddScore()
    {
        souls++;
        sceneSouls++;
    }

    public void ResetSceneSouls()
    {
        sceneSouls = 0;
    }
    public void ResetScore()
    {
        souls = souls - sceneSouls;
        sceneSouls = 0;
    }

    IEnumerator ShowUI()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }


}
